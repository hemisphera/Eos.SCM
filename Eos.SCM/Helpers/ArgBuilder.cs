using System;
using System.Collections.Generic;

namespace Eos.SCM.Helpers
{

  public sealed class ArgBuilder
  {
    readonly List<string> _arguments = new List<string>();


    public void Add(string value)
    {
      _arguments.Add(value);
    }

    public void AddIf(bool condition, string value)
    {
      if (!condition) return;
      Add(value);
    }

    public override string ToString()
    {
      return String.Join(" ", _arguments);
    }
  }

}
