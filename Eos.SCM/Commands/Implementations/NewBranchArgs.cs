using System.Management.Automation;

namespace Eos.SCM
{

  public class NewBranchArgs : ScmCommandArgsBase, INewBranchArgs
  {

    public string Name { get; set; }
    
    public SwitchParameter Permanent { get; set; }
    
    public SwitchParameter Commit { get; set; }

    public string TargetChangeset { get; set; }

  }

}