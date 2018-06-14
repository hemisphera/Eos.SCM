using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Get, ResourceNames.Branch)]
  public class GetBranchesCmdlet : ScmCmdletBase, IGetBranchesArgs
  {

    [Parameter]
    public SwitchParameter Current { get; set; }

    [Parameter]
    public SwitchParameter Permanent { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.GetBranches(this), true);
    }

  }

}
