using System;
using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.New, ResourceNames.Repository)]
  public class NewRepositoryCmdlet: ScmCmdletBase, INewRepositoryArgs
  {

    protected override void ProcessRecord()
    {
      Provider.CreateRepository(this);
    }

  }

}