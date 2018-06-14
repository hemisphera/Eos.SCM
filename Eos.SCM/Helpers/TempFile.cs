using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eos.SCM.Helpers
{

  public class TempFile : IDisposable
  {

    public string FilePath { get; private set; }


    public TempFile(string prefix = null)
    {
      FilePath = Path.Combine(Path.GetTempPath(), prefix + Guid.NewGuid().ToString().Substring(0, 8) + ".txt");
    }


    public void Dispose()
    {
      if (File.Exists(FilePath))
        File.Delete(FilePath);
    }

  }

}
