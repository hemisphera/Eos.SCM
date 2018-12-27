using System.Management.Automation;

namespace Eos.SCM
{

  public interface IMergeArgs : IScmCommandArgsBase
  {
    
    SwitchParameter NoCommit { get; }
    
    string OtherRev { get; }

  }

}