using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Get, ResourceNames.ChangeSet)]
  public class GetChangesetCmdlet : ScmCmdletBase, IGetChangesetArgs
  {

    [Parameter]
    public RevisionQuery Query { get; set; }

    [Parameter]
    public SwitchParameter Current { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.GetChangesets(this), true);
    }

  }

}