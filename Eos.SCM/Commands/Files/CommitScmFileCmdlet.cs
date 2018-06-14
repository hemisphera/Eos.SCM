using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet("Commit", ResourceNames.File)]
  public class CommitScmFileCmdlet : ScmCmdletBase, ICommitScmFileArgs
  {

    [Parameter]
    public string[] Files { get; set; }

    [Parameter(Mandatory = true)]
    public string Message { get; set; }

    [Parameter]
    public SwitchParameter All { get; set; }


    protected override void ProcessRecord()
    {
      Provider.Commit(this);
    }

  }

}
