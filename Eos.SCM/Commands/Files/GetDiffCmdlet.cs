using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Get, ResourceNames.Diff)]
  public class GetDiffCmdlet : ScmCmdletBase, IGetDiffArgs
  {

    [Parameter]
    public RevisionQuery Query { get; set; }

    [Parameter]
    public string[] Files { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.GetDiff(this), true);
    }
  }

}
