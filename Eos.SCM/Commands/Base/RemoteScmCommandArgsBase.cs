using System.Management.Automation;
using System.Net;

namespace Eos.SCM
{

  public abstract class RemoteScmCommandArgsBase : ScmCommandArgsBase, IRemoteScmCommandArgsBase
  {

    public ICredentials Credentials { get; set; }

    public SwitchParameter SkipCertificateValidation { get; set; }

  }

}
