using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eos.SCM.Helpers
{

  public static class PublicExtensions
  {

    public static void ThrowNotSupportedException(this IScmProvider provider, string message = "")
    {
      var msg = new StringBuilder();
      msg.AppendLine($"The requested operation is not supported by {provider.Name}");
      if (!String.IsNullOrEmpty(message))
        msg.AppendLine(message);
      throw new NotSupportedException(msg.ToString());
    }
    

    public static Changeset GetCurrentChangeset(this IScmProvider provider)
    {
      return provider.GetChangesets(new GetChangesetsArgs
      {
        Current = true
      }).LastOrDefault();
    }

    public static Changeset GetChangeset(this IScmProvider provider, string id)
    {
      return provider.GetChangesets(new GetChangesetsArgs
      {
        Query = id
      }).LastOrDefault();
    }

  }

}
