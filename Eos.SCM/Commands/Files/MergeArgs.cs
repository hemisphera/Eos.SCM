using System.Management.Automation;

namespace Eos.SCM
{

  public class MergeArgs : ScmCommandArgsBase, IMergeArgs
  {

    public SwitchParameter NoCommit { get; set; }
    
    public string OtherRev { get; set; }

  }

}
