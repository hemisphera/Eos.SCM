using System.Management.Automation;

namespace Eos.SCM
{

  public class GetChangesetArgs : ScmCommandArgsBase, IGetChangesetArgs
  {

    public RevisionQuery Query { get; set; }

    public SwitchParameter Current { get; set; }

  }

}
