using System.Management.Automation;

namespace Eos.SCM
{

  public interface IUploadChangesetArgs : IRemoteScmCommandArgsBase
  {

    SwitchParameter CheckOnly { get; }

    string Destination { get; }

    string TargetRev { get; }

  }

}