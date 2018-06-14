using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eos.SCM
{

  public class ScmMergeTool
  {

    public string Name { get; set; }

    public string Filename { get; set; }

    public string Arguments { get; set; }

    public int Priority { get; set; }

    public bool Premerge { get; set; }

    public bool Gui { get; set; }

  }

}
