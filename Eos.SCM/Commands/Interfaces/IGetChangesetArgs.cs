using System.Management.Automation;

namespace Eos.SCM
{

  public interface IGetChangesetArgs : IScmCommandArgsBase
  {

    RevisionQuery Query { get; }

    SwitchParameter Current { get; }

  }

}
