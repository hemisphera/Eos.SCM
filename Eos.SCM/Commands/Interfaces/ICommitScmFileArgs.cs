using System.Management.Automation;

namespace Eos.SCM
{

  public interface ICommitScmFileArgs : IScmCommandArgsBase
  {

    string[] Files { get; }

    string Message { get; }

    SwitchParameter All { get; }

  }

}
