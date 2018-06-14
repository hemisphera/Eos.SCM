using System.Net;

namespace Eos.SCM
{

  public class ScmCommandArgs : IScmCommandArgs
  {

    public string RepositoryPath { get; set; }

    public ICredentials Credentials { get; set; }

  }

}
