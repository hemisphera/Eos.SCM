using System.Net;

namespace Eos.SCM
{

  public class GetDiffArgs : ScmCommandArgsBase, IGetDiffArgs
  {

    public RevisionQuery Query { get; set; }

    public string[] Files { get; set; }

  }

}
