using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using eos.SCM;
using Eos.SCM.Helpers;
using Eos.SCM.Providers;

namespace Eos.SCM
{

  public class GitScmProvider : ScmProviderBase, IScmProvider
  {

    private const string XmlLogTemplate =
      "<Node><ID>%H</ID><No>-1</No><Date>%cI</Date><User>%cn</User><Comment>%s</Comment></Node>"; 
      

    public string Name => "Git";

    public string UserConfigFilename => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".gitconfig");


    public GitScmProvider()
    {
      DefaultExeName = "git.exe";
    }

        
    private ScmRemote GetDefaultRemote(string repositoryPath)
    {
      var remotes = GetRemotes(new GetItemArgs<ScmRemote>
      {
        RepositoryPath = repositoryPath,
      });
      return remotes.FirstOrDefault();
    }
    
    private string GetRepositoryRoot(string folder)
    {
      var ab = new ArgBuilder();
      ab.Add("rev-parse");
      ab.Add("--show-toplevel");
      return RunCommand(ab, null, folder).LastOrDefault();
    }

    private void Reset(string repositoryPath)
    {
      var ab = new ArgBuilder();
      ab.Add("reset");
      RunCommand(ab, repositoryPath);
    }

    private void AddFileToIndex(string repositoryPath, string filename)
    {
      var ab = new ArgBuilder();
      ab.Add("add");
      ab.Add(filename);
      RunCommand(ab, repositoryPath);
    }
    
    private void ThrowPermanentBranchException()
    {
      throw new NotSupportedException($"{Name} does not support permanent branches.");
    }


    public void Commit(ICommitScmFileArgs args)
    {
      Reset(args.RepositoryPath);

      if (!args.All)
      {
        foreach (var file in args.Files)
          AddFileToIndex(args.RepositoryPath, file);
      }
      var ab = new ArgBuilder();
      ab.Add("commit");
      ab.AddIf(args.All, "--all");

      using (var msgFile = new TempFile("msg"))
      {
        File.WriteAllText(msgFile.FilePath, args.Message);
        ab.Add($"--file=\"{msgFile}\"");
        RunCommand(ab, args);
      }
    }

    public ScmFile[] GetFiles(IGetScmFileArgs args)
    {
      throw new NotImplementedException();
    }

    public void Revert(IResetScmFilesArgs args)
    {
      throw new NotImplementedException();
    }

    public void RenameScmFile(IRenameScmFileArgs args)
    {
      throw new NotImplementedException();
    }

    public Changeset[] DownloadChangesets(IDownloadChangesetArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("fetch");
      ab.Add(args.Source);
      ab.Add(args.TargetRev);
      RunCommand(ab, args);

      return GetChangesets(new GetChangesetArgs
      {
        RepositoryPath = args.RepositoryPath,
        Query = "..@{u}"
      });
    }

    public Changeset[] UploadChangesets(IUploadChangesetArgs args)
    {
      var currentBranch = GetBranches(new GetBranchesArgs
      {
        Current = true,
        RepositoryPath = args.RepositoryPath
      }).FirstOrDefault();

      var targetRev = args.TargetRev.WhenNullOrEmpty(currentBranch);
      var destination = args.Destination.WhenNullOrEmpty("origin");
      var query = new RevisionQuery
      {
        FromRevisionId = targetRev,
        ToRevisionId = $"{destination}/{targetRev}"
      };

      var changeSets =
        GetChangesets(new GetChangesetArgs
        {
          RepositoryPath = args.RepositoryPath,
          Query = query
        });

      if (!args.CheckOnly)
      {
        var ab = new ArgBuilder();
        ab.Add("push");
        ab.Add(args.Destination);
        ab.Add(args.TargetRev);
      }
        
      return changeSets;
    }

    public FileDiff[] GetDiff(IGetDiffArgs args)
    {
      throw new NotImplementedException();
    }

    public Changeset[] GetChangesets(IGetChangesetArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("log");
      ab.Add(args.Current ? "-n 1" : FormatQuery(args.Query));
      ab.Add($"--pretty=\"{XmlLogTemplate}\"");
      return Changeset.FromXml(RunCommand(ab, args));
    }

    public void CheckoutChangeset(ICheckoutChangesetArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("checkout");
      ab.Add(args.TargetRev);
      ab.AddIf(args.Force, "--force");
      ab.AddIf(!String.IsNullOrEmpty(args.NewBranchName), $"-b \"{args.NewBranchName}\"");
      RunCommand(ab, args);
    }

    public byte[] GetFileContents(IGetScmFileContentArgs args)
    {
      throw new NotImplementedException();
    }

    public void CreateRepository(INewRepositoryArgs args)
    {
      Directory.CreateDirectory(args.RepositoryPath);
      var ab = new ArgBuilder();
      ab.Add("init");
      RunCommand(ab, args.RepositoryPath);
    }

    public void CloneRepository(ICloneRepositoryArgs args)
    {
      throw new NotImplementedException();
    }

    public ScmRemote[] GetRemotes(IGetItemArgs<ScmRemote> args)
    {
      throw new NotImplementedException();
    }

    public void AddRemote(IAddItemsArgs<ScmRemote> args)
    {
      throw new NotImplementedException();
    }

    public void RemoveRemote(IRemoveItemArgs<ScmRemote> args)
    {
      throw new NotImplementedException();
    }

    public void CreateBranch(INewBranchArgs args)
    {
      throw new NotImplementedException();
    }

    public void RemoveBranch(IRemoveBranchArgs args)
    {
      throw new NotImplementedException();
    }

    public string[] GetBranches(IGetBranchesArgs args)
    {
      if (args.Permanent)
        ThrowPermanentBranchException();

      var ab = new ArgBuilder();
      ab.Add("branch");
      var branches = RunCommand(ab, args)
        .Select(line => Regex.Match(line, @"^(?<active>\*)? *(?<name>.*)$"));
      if (args.Current)
        branches = branches.Where(b => b.Groups["active"].Value == "*");
      return branches.Select(b => b.Groups["name"].Value).ToArray();
    }

    public ScmMergeTool[] GetMergeTools(IGetItemArgs<ScmMergeTool> args)
    {
      throw new NotImplementedException();
    }

    public void AddMergeTools(IAddItemsArgs<ScmMergeTool> args)
    {
      throw new NotImplementedException();
    }

    public void RemoveMergeTools(IRemoveItemArgs<ScmMergeTool> args)
    {
      throw new NotImplementedException();
    }

    public bool IsRepository(string repositoryPath)
    {
      try
      {
        GetRepositoryRoot(repositoryPath);
        return true;
      }
      catch
      {
        return false;
      }
    }


    public bool IsError(int exitCode)
    {
      return exitCode != 0;
    }

    public string FormatCredentials(ICredentials credentials)
    {
      if (credentials == null) 
        return null;

      throw new NotImplementedException();
    }

    public string FormatQuery(RevisionQuery query)
    {
      if (query == null)
        return "";
      return $"{query.ToRevisionId}..{query.FromRevisionId}";
    }

  }

}
