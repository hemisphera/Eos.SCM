using System.Management.Automation;

namespace Eos.SCM
{

  public abstract class ScmCommandArgsBase : IScmCommandArgsBase
  {

    public string RepositoryPath { get; set; }
    
    public SwitchParameter Force { get; set; }

  }

}
