using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.New, ResourceNames.Branch)]
  public class NewBranchCmdlet : ScmCmdletBase, INewBranchArgs
  {

    [Parameter(Mandatory = true, Position = 0)]
    public string Name { get; set; }

    [Parameter]
    public SwitchParameter Permanent { get; set; }

    [Parameter]
    public SwitchParameter Commit { get; set; }

    [Parameter]
    public string TargetChangeset { get; set; }

    protected override void ProcessRecord()
    {
      Provider.CreateBranch(this);
    }

  }

}