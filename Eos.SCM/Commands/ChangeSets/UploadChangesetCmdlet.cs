using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet("Upload", "Changeset")]
  public class UploadChangesetCmdlet : RemoteScmCmdletBase, IUploadChangesetArgs
  {

    [Parameter]
    public bool CheckOnly { get; set; }

    [Parameter]
    public string Destination { get; set; }

    [Parameter]
    public RevisionQuery Query { get; set; }


    protected override void ProcessRecord()
    {
      Provider.UploadChangesets(this);
    }

  }

}