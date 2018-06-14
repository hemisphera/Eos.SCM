using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eos.SCM.Commands
{

  public class GetScmFileArgs : ScmCommandArgsBase, IGetScmFileArgs
  {

    public bool All { get; set; }

    public string[] Files { get; set; }

    public RevisionQuery Query { get; set; }

  }

}
