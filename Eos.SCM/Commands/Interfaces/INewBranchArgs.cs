using System.Management.Automation;
using System.Net;

namespace Eos.SCM
{

  public interface INewBranchArgs : IScmCommandArgsBase
  {

    string Name { get; }

    SwitchParameter Permanent { get; }

    SwitchParameter Commit { get; }

    string TargetChangeset { get; }

  }

}
