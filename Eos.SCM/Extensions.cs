using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Eos.SCM.Helpers;

namespace Eos.SCM
{

  public static class Extensions
  {

    public static ScmRemote[] GetRemotes(this IScmProvider prov, GetRemotesArgs e)
    {
      return prov.GetRemotes(e);
    }

    internal static IEnumerable<T> InKeys<T>(this IEnumerable<T> coll, Func<T, string> keySelector, IEnumerable<string> keys)
    {
      var keyArray = keys.ToArray();
      if (!keyArray.Any())
        return coll;
      return coll.Where(i => keyArray.Contains(keySelector(i)));
    }



  }

}
