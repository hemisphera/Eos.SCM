using System.Management.Automation;

namespace Eos.SCM
{

  public class UploadChangesetArgs : RemoteScmCommandArgsBase, IUploadChangesetArgs
  {

    public SwitchParameter CheckOnly { get; set; }

    public string Destination { get; set; }

    public string TargetRev { get; set; }

  }

}
