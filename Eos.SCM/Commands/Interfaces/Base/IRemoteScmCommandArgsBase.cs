using System.Management.Automation;
using System.Net;

namespace Eos.SCM
{

  public interface IRemoteScmCommandArgsBase : IScmCommandArgsBase
  {

    ICredentials Credentials { get; }

    SwitchParameter SkipCertificateValidation { get; }

  }

}
