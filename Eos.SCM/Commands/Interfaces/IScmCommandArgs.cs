using System.Net;

namespace Eos.SCM
{

  public interface IScmCommandArgs
  {

    string RepositoryPath { get; }

    ICredentials Credentials { get; }

  }

}
