using eos.SCM;
using Eos.SCM.Helpers;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Eos.SCM.Providers;

namespace Eos.SCM
{

  public class HgScmProvider : ScmProviderBase, IScmProvider
  {

    private const string XmlLogTemplate = 
      "<Node><ID>{node}</ID><No>{rev}</No><Date>{date|rfc3339date}</Date><User>{author|escape}</User><Comment>{desc|escape}</Comment>" + 
      "<Parent1>{p1node}</Parent1><Parent2>{p2node}</Parent2></Node>";


    public string Name => "Mercurial";

    public string UserConfigFilename => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "mercurial.ini");


    public HgScmProvider()
    {
      DefaultExeName = "hg.exe";
    }


    private static void DisposeTempFile(string filename)
    {
      if (String.IsNullOrEmpty(filename) || !File.Exists(filename))
        return;
      File.Delete(filename);
    }

    private string GetRepositoryRoot(string folder)
    {
      var ab = new ArgBuilder();
      ab.Add("root");
      return RunCommand(ab, null, folder).LastOrDefault();
    }

    private string GetRepositoryStore(string folder)
    {
      return Path.Combine(GetRepositoryRoot(folder), ".hg");
    }

    private Changeset[] ParseChangesets(string[] strings)
    {
      throw new NotImplementedException();
    }

    private static FileIniDataParser CreateIniParser()
    {
      var iniReader = new FileIniDataParser();
      iniReader.Parser.Configuration.CommentString = "#";
      return iniReader;
    }

    private static IniData LoadIniFile(string filename)
    {
      var iniReader = CreateIniParser();
      return iniReader.ReadFile(filename);
    }

    private static void SaveIniFile(string filename, IniData data)
    {
      var iniReader = CreateIniParser();
      iniReader.WriteFile(filename, data);
    }


    public void CreateRepository(INewRepositoryArgs args)
    {
      Directory.CreateDirectory(args.RepositoryPath);
      var ab = new ArgBuilder();
      ab.Add("init");
      ab.Add(args.RepositoryPath.Enclose('"'));
      RunCommand(ab, args);
    }

    public void CloneRepository(ICloneRepositoryArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("clone");
      ab.Add(args.Source.Enclose('"'));
      ab.Add(args.RepositoryPath.Enclose('"'));
      RunCommand(ab, args);
    }


    public ScmRemote[] GetRemotes(IGetItemArgs<ScmRemote> args)
    {
      var hgrcFilename = Path.Combine(GetRepositoryStore(args.RepositoryPath), "hgrc");
      var iniData = LoadIniFile(hgrcFilename);
      var remotes = iniData["paths"]
        .Select(e => new ScmRemote
        {
          Name = e.KeyName,
          Url = e.Value
        });

      remotes = remotes.Where(e => args.Key.Contains(e.Name));
      return remotes.ToArray();
    }

    public void AddRemote(IAddItemsArgs<ScmRemote> args)
    {
      var hgrcFilename = Path.Combine(GetRepositoryStore(args.RepositoryPath), "hgrc");
      var iniData = LoadIniFile(hgrcFilename);
      var section = iniData["paths"];
      foreach (var item in args.Items)
        section.AddKey(item.Name, item.Url);
      SaveIniFile(hgrcFilename, iniData);
    }

    public void RemoveRemote(IRemoveItemArgs<ScmRemote> args)
    {
      var hgrcFilename = Path.Combine(GetRepositoryStore(args.RepositoryPath), "hgrc");
      var iniData = LoadIniFile(hgrcFilename);
      var paths = iniData["paths"];
      foreach (var key in args.Key)
        if (paths.ContainsKey(key))
          paths.RemoveKey(key);
      SaveIniFile(hgrcFilename, iniData);
    }

    public void Commit(ICommitScmFileArgs args)
    {
      var ab = new ArgBuilder();
      TempFile fileList = null;
      ab.Add("commit");
      if (args.All)
        ab.Add("--addremove");
      else
        if (args.Files?.Any() == true)
        {
          fileList = CreateFileList(args.Files);
          ab.Add($"--include listfile:\"{fileList}\"");
        }

      try
      {
        using (var msgFile = new TempFile("msg"))
        {
          File.WriteAllText(msgFile.FilePath, args.Message);
          ab.Add($"--logfile \"{msgFile}\"");
          RunCommand(ab, args);
        }
      }
      finally
      {
        fileList?.Dispose();
      }
    }

    public ScmFile[] GetFiles(IGetScmFileArgs args)
    {
      var command = args.All ? "files" : "status";

      var ab = new ArgBuilder();
      ab.Add(command);
      TempFile fileList = null;
      if (args.Files != null)
        if (args.Files.Any())
        {
          fileList = CreateFileList(args.Files);
          ab.Add($"--include listfile:\"{fileList}\"");
        }

      string[] files;
      try
      {
        if (args.Query != null)
          ab.Add($"--rev \"{FormatQuery(args.Query)}\"");
        files = RunCommand(ab, args);
      }
      finally
      {
        fileList?.Dispose();
      }

      var dict = new Dictionary<char, ScmFileStatus>
      {
        {'C', ScmFileStatus.Clean},
        {'M', ScmFileStatus.Modified},
        {'A', ScmFileStatus.Added},
        {'R', ScmFileStatus.Removed},
        {'?', ScmFileStatus.Unknown},
        {'!', ScmFileStatus.Missing},
      };

      return files.Select(file =>
        {
          var status = dict[file[0]];
          var localFilename = file.Substring(2);
          return new ScmFile(args.RepositoryPath, localFilename, status);
        }).ToArray();
    }

    public void Revert(IRevertScmFilesArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("revert");
      var fileList = CreateFileList(args.Files);
      ab.Add($"--include listfile:\"{fileList}\"");
      RunCommand(ab, args);
    }

    public void RenameScmFile(IRenameScmFileArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("rename");
      if (args.MarkOnly)
        ab.Add("--after");
      ab.Add(args.Filename.Enclose('"'));
      ab.Add(args.NewFilename.Enclose('"'));
      RunCommand(ab, args);
    }


    public FileDiff[] GetDiff(IGetDiffArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("diff");
      TempFile fileList = null;
      if (args.Files != null)
        if (args.Files.Any())
        {
          fileList = CreateFileList(args.Files);
          ab.Add($"--include listfile:\"{fileList}\"");
        }

      try
      {
        if (args.Query != null)
          ab.Add($"--rev \"{FormatQuery(args.Query)}\"");
        var lines = RunCommand(ab, args);
        return FileDiff.FromLines(lines);
      }
      finally
      {
        fileList?.Dispose();
      }
    }

    public Changeset[] GetChangesets(IGetChangesetArgs args)
    {
      var ab = new ArgBuilder();
      if (args.Current)
        ab.Add("parent");
      else
      {
        ab.Add("log");
        if (args.Query != null)
          ab.Add($"--rev \"{FormatQuery(args.Query)}\"");
      }
      ab.Add($"--template \"{XmlLogTemplate}\"");
      var lines = RunCommand(ab, args);
      return Changeset.FromXml(lines);
    }

    public Changeset[] DownloadChangesets(IDownloadChangesetArgs args)
    {
      if (String.IsNullOrEmpty(args.Source))
        throw new ArgumentNullException(nameof(args.Source));

      var ab = new ArgBuilder();
      ab.Add(args.CheckOnly ? "incoming" : "pull");
      ab.Add(args.Source);
      if (args.Query != null)
        ab.Add($"--rev {FormatQuery(args.Query)}");

      var r = RunCommand(ab, args);

      return args.CheckOnly ? ParseChangesets(r) : new Changeset[] { };
    }

    public Changeset[] UploadChangesets(IUploadChangesetArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add(args.CheckOnly ? "outgoing" : "push");
      ab.Add(args.Destination);
      if (args.Query != null)
        ab.Add($"--rev {FormatQuery(args.Query)}");

      var r = RunCommand(ab, args);

      return args.CheckOnly ? ParseChangesets(r) : new Changeset[] { };
    }

    public void CheckoutChangeset(ICheckoutChangesetArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("update");
      if (args.Clean)
        ab.Add("--clean");
      if (args.TargetRevision != null)
        ab.Add(args.TargetRevision.FromRevisionId);
      RunCommand(ab, args);

      if (!String.IsNullOrEmpty(args.NewBranchName))
        CreateBranch(new NewBranchArgs
        {
          Name = args.NewBranchName,
          RepositoryPath = args.RepositoryPath
        });
    }

    public byte[] GetFileContents(IGetScmFileContentArgs args)
    {
      using (var tempFile = new TempFile())
      {
        var ab = new ArgBuilder();
        ab.Add("cat");
        ab.Add(args.File);
        ab.Add($"--output \"{tempFile}\"");
        RunCommand(ab, args);
        return File.ReadAllBytes(tempFile.FilePath);
      }
    }


    private void CreatePermanentBranch(INewBranchArgs args)
    {
      if (!String.IsNullOrEmpty(args.TargetChangeset))
        throw new NotSupportedException($"'{nameof(args.TargetChangeset)}' is not supported for permanent branches.");

      var ab = new ArgBuilder();
      ab.Add("branch");
      ab.Add($"\"{args.Name}\"");
      ab.AddIf(args.Force, "--force");
      RunCommand(ab, args);

      if (args.Commit)
        Commit(new CommitScmFileArgs
        {
          RepositoryPath = args.RepositoryPath,
          Message = $"Opened branch '{args.Name}'"
        });
    }

    private void CreateBookmark(INewBranchArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add("bookmarks");
      if (!String.IsNullOrEmpty(args.TargetChangeset))
        ab.Add($"--rev \"{args.TargetChangeset}\"");
      ab.AddIf(args.Force, "--force");
      ab.Add(args.Name);
      RunCommand(ab, args);
    }

    public void CreateBranch(INewBranchArgs args)
    {
      if (args.Permanent)
        CreatePermanentBranch(args);
      else
        CreateBookmark(args);
    }

    public void RemoveBranch(IRemoveBranchArgs args)
    {
      this.ThrowNotSupportedException();
    }

    public string[] GetBranches(IGetBranchesArgs args)
    {
      var ab = new ArgBuilder();
      ab.Add(args.Current ? "branch" : "branches");

      IEnumerable<string> result = RunCommand(ab, args);
      if (!args.Current)
      {
        result = result.Select(r =>
        {
          var m = Regex.Match(r, @"^(?<name>.*?)\s+[0-9]+:[a-f0-9]+(?<inactive> \(inactive\))?");
          return !m.Success ? null : m.Groups["name"].Value;
        });
      }
      return  result.Where(r => !String.IsNullOrEmpty(r)).ToArray();
    }


    public ScmMergeTool[] GetMergeTools(IGetItemArgs<ScmMergeTool> args)
    {
      var data = LoadIniFile(UserConfigFilename)["merge-tools"];
      if (data == null)
        return new ScmMergeTool[] { };

      var tools = new Dictionary<string, ScmMergeTool>();
      foreach (var kvp in data)
      {
        string toolName;
        string paramName;
        try
        {
          var parts = kvp.KeyName.Split('.');
          toolName = parts[0];
          paramName = parts[1];
        }
        catch
        {
          continue;
        }

        ScmMergeTool tool;
        if (tools.ContainsKey(toolName))
          tool = tools[toolName];
        else
        {
          tool = new ScmMergeTool
          {
            Name = toolName
          };
          tools.Add(tool.Name, tool);
        }
        const StringComparison ic = StringComparison.OrdinalIgnoreCase;
        if (paramName.Equals("gui", ic))
          tool.Gui = kvp.Value.Equals("true", ic);
        if (paramName.Equals("premerge", ic))
          tool.Premerge = kvp.Value.Equals("true", ic);
        if (paramName.Equals("executable", ic))
          tool.Filename = kvp.Value;
        if (paramName.Equals("args", ic))
          tool.Arguments = kvp.Value;
        if (paramName.Equals("priority", ic))
        {
          if (int.TryParse(kvp.Value, out var v))
            tool.Priority = v;
        }
      }

      return tools.Values.InKeys(t => t.Name, args.Key).ToArray();
    }

    public void AddMergeTools(IAddItemsArgs<ScmMergeTool> args)
    {
      var data = LoadIniFile(UserConfigFilename);
      var section = data["merge-tools"];
      foreach (var mt in args.Items)
      {
        section.AddKey($"{mt.Name}.gui", mt.Gui ? "true" : "false");
        section.AddKey($"{mt.Name}.premerge", mt.Premerge ? "true" : "false");
        section.AddKey($"{mt.Name}.executable", mt.Filename);
        section.AddKey($"{mt.Name}.args", mt.Arguments);
        if (mt.Priority > 0)
          section.AddKey($"{mt.Name}.priority", mt.Priority.ToString());
      }
      SaveIniFile(UserConfigFilename, data);
    }

    public void RemoveMergeTools(IRemoveItemArgs<ScmMergeTool> args)
    {
      var data = LoadIniFile(UserConfigFilename);
      var section = data["merge-tools"];
      foreach (var name in args.Key)
      {
        var keysToRemove = section.Select(kd => kd.KeyName).Where(k => k.StartsWith($"{name}.")).ToArray();
        foreach (var keyToRemove in keysToRemove)
          section.RemoveKey(keyToRemove);
      }
      SaveIniFile(UserConfigFilename, data);
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
      return !(new[] {0, 1}.Contains(exitCode));
    }

    public string FormatCredentials(ICredentials credentials)
    {
      var c = credentials.GetCredential(new Uri("http://localhost"), "Basic");
      var b = new StringBuilder();
      var cfg = "--config \"auth.scm.{0}={1}\" ";
      b.AppendFormat(cfg, "prefix", "*");
      if (!String.IsNullOrEmpty(c.Domain))
        b.AppendFormat(cfg, "username", c.Domain + "\\" + c.UserName);
      else
        b.AppendFormat(cfg, "username", c.UserName);
      b.AppendFormat(cfg, "password", c.Password);
      b.AppendFormat(cfg, "schemes", "http https");
      return b.ToString();
    }

    public string FormatQuery(RevisionQuery query)
    {
      if (!String.IsNullOrEmpty(query.ToRevisionId))
        return $"{query.FromRevisionId}:{query.ToRevisionId}";
      return query.FromRevisionId;
    }

  }

}
