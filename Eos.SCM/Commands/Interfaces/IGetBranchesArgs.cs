using System.Management.Automation;

namespace Eos.SCM
{

  public interface IGetBranchesArgs : IScmCommandArgsBase
  {
    
    SwitchParameter Current { get; }

    SwitchParameter Permanent { get; }

  }

}