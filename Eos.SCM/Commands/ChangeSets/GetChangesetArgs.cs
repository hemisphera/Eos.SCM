using System.Management.Automation;

namespace Eos.SCM
{

  public class GetChangesetsArgs : ScmCommandArgsBase, IGetChangesetsArgs
  {

    public RevisionQuery Query { get; set; }

    public SwitchParameter Current { get; set; }

  }

}
