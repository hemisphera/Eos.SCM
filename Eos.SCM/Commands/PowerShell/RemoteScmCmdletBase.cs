using System.Management.Automation;
using System.Net;

namespace Eos.SCM
{

  public abstract class RemoteScmCmdletBase : ScmCmdletBase, IRemoteScmCommandArgsBase
  {

    [Parameter]
    public ICredentials Credentials { get; set; }

    [Parameter]
    public SwitchParameter SkipCertificateValidation { get; set; }

  }

}
