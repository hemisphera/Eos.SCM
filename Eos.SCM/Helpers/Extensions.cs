using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eos.SCM.Helpers
{

  internal static class Extensions
  {

    internal static IEnumerable<T> InKeys<T>(this IEnumerable<T> coll, Func<T, string> keySelector, IEnumerable<string> keys)
    {
      var keyArray = keys.ToArray();
      if (!keyArray.Any())
        return coll;
      return coll.Where(i => keyArray.Contains(keySelector(i)));
    }

    public static string Enclose(this string inStr, char delimiter)
    {
      return delimiter + inStr + delimiter;
    }

    public static bool TestValue<T>(string name, T value, bool withError = false)
    {
      var isValid = (value.Equals(default(T)));
      if (withError && !isValid)
        throw new ArgumentNullException(name);
      return isValid;
    }
  }

}
