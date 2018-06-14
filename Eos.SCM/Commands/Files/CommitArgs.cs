using System.Management.Automation;
using System.Net;

namespace Eos.SCM
{

  public class CommitScmFileArgs : ScmCommandArgsBase, ICommitScmFileArgs
  {

    public string[] Files { get; set; }

    public string Message { get; set; }

    public SwitchParameter All { get; set; }

  }

}
