using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eos.SCM
{

  public sealed class ScmProviderFactory
  {

    private static ScmProviderFactory _instance;

    public static ScmProviderFactory Instance => _instance ?? (_instance = new ScmProviderFactory());

    public List<IScmProvider> Providers { get; }


    private ScmProviderFactory()
    {
      Providers = new List<IScmProvider>
      {
        new HgScmProvider()
      };
    }


    public IScmProvider FindProviderByPath(string repositoryPath)
    {
      foreach (var provider in Providers)
      {
        if (provider.IsRepository(repositoryPath))
          return provider;
      }
      throw new NotSupportedException("Not a valid repository or no SCM provider found that supports this repository.");
    }

    public IScmProvider FindProviderByName(string name)
    {
      var provider = Providers.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
      if (provider != null)
        return provider;
      throw new NotSupportedException($"No SCM provider found with name '{name}'.");
    }

  }

}
