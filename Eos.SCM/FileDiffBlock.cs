using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace eos.SCM
{

  public class FileDiffBlock
  {
    public int OldFileStartIndex { get; }
    public int NewFileStartIndex { get; }
    public string Header { get; private set; }

    readonly List<string> _lines = new List<string>();
    public ReadOnlyCollection<string> Lines => _lines.AsReadOnly();

    internal FileDiffBlock(IEnumerable<string> lines)
    {
      var l = lines.ToArray();
      Header = l.First();
      _lines.AddRange(l.Skip(1));
    }

  }

}
