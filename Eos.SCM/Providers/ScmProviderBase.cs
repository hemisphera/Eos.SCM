using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Eos.SCM.Helpers;

namespace Eos.SCM.Providers
{

  public abstract class ScmProviderBase
  {

    protected string DefaultExeName { get; set; }

    
    protected string[] RunCommand(ArgBuilder args, string workingDir)
    {
      return RunCommand(DefaultExeName, args, workingDir);
    }

    protected string[] RunCommand(ArgBuilder args, IScmCommandArgsBase cmd)
    {
      return RunCommand(DefaultExeName, args, cmd);
    }

    protected string[] RunCommand(ArgBuilder args, ICredentials creds, string workingDir)
    {
      return RunCommand(DefaultExeName, args, creds, workingDir);
    }
    
    protected string[] RunCommand(string exeName, ArgBuilder args, string workingDir)
    {
      return RunCommand(exeName, args, null, workingDir);
    }

    protected string[] RunCommand(string exeName, ArgBuilder args, IScmCommandArgsBase cmd)
    {
      var credentials = (cmd as IRemoteScmCommandArgsBase)?.Credentials;
      return RunCommand(exeName, args, credentials, cmd.RepositoryPath);
    }

    protected string[] RunCommand(string exeName, ArgBuilder args, ICredentials creds, string workingDir)
    {
      var provider = this as IScmProvider;
      if (String.IsNullOrEmpty(exeName))
        exeName = DefaultExeName;

      var h = new CmdLineHelper(workingDir)
      {
        CredentialsString = provider?.FormatCredentials(creds)
      };
      var result = h.Call(exeName, args.ToString());

      if (provider != null)
        if (provider.IsError(h.LastExitCode))
          throw new Exception($"SCM provider {provider.Name} returned exit code '{h.LastExitCode}'.");

      return result;
    }


    protected TempFile CreateFileList(IEnumerable<string> files)
    {
      var tmpFile = new TempFile("fl_");
      var fileList = String.Join("\n", files);
      File.WriteAllText(tmpFile.FilePath, fileList, Encoding.ASCII);
      return tmpFile;
    }

  }

}
