using System.Management.Automation;

namespace Eos.SCM
{

  public interface IGetChangesetsArgs : IScmCommandArgsBase
  {

    RevisionQuery Query { get; }

    SwitchParameter Current { get; }

  }

}
