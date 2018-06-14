using System.Management.Automation;
using System.Net;

namespace Eos.SCM
{

  public interface IScmCommandArgsBase
  {

    string RepositoryPath { get; }

    SwitchParameter Force { get; }

  }

}
