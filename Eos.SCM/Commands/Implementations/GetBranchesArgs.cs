using System.Management.Automation;

namespace Eos.SCM
{

  public class GetBranchesArgs : ScmCommandArgsBase, IGetBranchesArgs
  {

    public SwitchParameter Current { get; set; }
    
    public SwitchParameter Permanent { get; set; }

  }

}
