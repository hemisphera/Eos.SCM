using System;
using System.Management.Automation;
using System.Net;

namespace Eos.SCM
{

  public abstract class ScmCmdletBase : PSCmdlet, IScmCommandArgsBase
  {

    [Parameter]
    public string RepositoryPath { get; set; }

    [Parameter]
    public SwitchParameter Force { get; set; }

    [Parameter]
    public IScmProvider Provider { get; set; }

    [Parameter]
    public string ProviderName { get; set; }


    protected override void BeginProcessing()
    {
      SetRepositoryPath();

      if (Provider == null && !String.IsNullOrEmpty(ProviderName))
        Provider = ScmProviderFactory.Instance.FindProviderByName(ProviderName);
      if (Provider == null && !String.IsNullOrEmpty(RepositoryPath))
        Provider = ScmProviderFactory.Instance.FindProviderByPath(RepositoryPath);
      if (Provider == null)
        throw new ArgumentNullException(nameof(Provider));
    }

    protected void SetRepositoryPath()
    {
      if (String.IsNullOrEmpty(RepositoryPath))
        RepositoryPath = this.SessionState.Path.CurrentFileSystemLocation.Path;
    }

  }

}
