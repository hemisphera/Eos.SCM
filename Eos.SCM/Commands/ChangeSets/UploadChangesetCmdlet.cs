using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet("Upload", "Changeset")]
  public class UploadChangesetCmdlet : RemoteScmCmdletBase, IUploadChangesetArgs
  {

    [Parameter]
    public SwitchParameter CheckOnly { get; set; }

    [Parameter]
    public string Destination { get; set; }

    [Parameter]
    public string TargetRev { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.UploadChangesets(this), true);
    }

  }

}