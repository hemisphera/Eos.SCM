using System;
using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.New, ResourceNames.Repository)]
  public class NewRepositoryCmdlet: ScmCmdletBase, INewRepositoryArgs
  {

    [Parameter(Position = 0)]
    public string Location
    {
      get => RepositoryPath;
      set => RepositoryPath = value;
    }

    protected override void ProcessRecord()
    {
      Provider.CreateRepository(this);
    }

  }

}