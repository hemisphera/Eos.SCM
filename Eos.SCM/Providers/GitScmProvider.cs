using System;
using System.IO;
using System.Net;
using eos.SCM;
using Eos.SCM.Helpers;
using Eos.SCM.Providers;

namespace Eos.SCM
{

  public class GitScmProvider : ScmProviderBase, IScmProvider
  {

    public string Name => "Git";

    public string UserConfigFilename => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".gitconfig");


    public GitScmProvider()
    {
      DefaultExeName = "git.exe";
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

    public void Revert(IRevertScmFilesArgs args)
    {
      throw new NotImplementedException();
    }

    public void RenameScmFile(IRenameScmFileArgs args)
    {
      throw new NotImplementedException();
    }

    public Changeset[] DownloadChangesets(IDownloadChangesetArgs args)
    {
      throw new NotImplementedException();
    }

    public Changeset[] UploadChangesets(IUploadChangesetArgs args)
    {
      throw new NotImplementedException();
    }

    public FileDiff[] GetDiff(IGetDiffArgs args)
    {
      throw new NotImplementedException();
    }

    public Changeset[] GetChangesets(IGetChangesetArgs args)
    {
      throw new NotImplementedException();
    }

    public void CheckoutChangeset(ICheckoutChangesetArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("checkout");
      ab.Add(args.TargetRevision.FromRevisionId);
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
      throw new NotImplementedException();
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
      throw new NotImplementedException();
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
      throw new NotImplementedException();
    }

  }

}
