using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Remove, ResourceNames.Branch)]
  public class RemoveBranchCmdlet : ScmCmdletBase, IRemoveBranchArgs
  {

    [Parameter(Mandatory = true, Position = 0)]
    public string Name { get; set; }


    protected override void ProcessRecord()
    {
      Provider.RemoveBranch(this);
    }

  }

}