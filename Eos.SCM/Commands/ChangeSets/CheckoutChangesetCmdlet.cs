using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet("Checkout", ResourceNames.ChangeSet)]
  public class CheckoutChangesetCmdlet : ScmCmdletBase, ICheckoutChangesetArgs
  {

    [Parameter(Mandatory = true, Position = 0)]
    public string TargetRev { get; set; }

    [Parameter]
    public string NewBranchName { get; set; }

    [Parameter]
    public bool Clean { get; set; }


    protected override void ProcessRecord()
    {
      Provider.CheckoutChangeset(this);
    }

  }

}