using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet("Clone", ResourceNames.Repository)]
  public class CloneRepositoryCmdlet : RemoteScmCmdletBase, ICloneRepositoryArgs
  {

    [Parameter]
    public string Source { get; set; }


    protected override void ProcessRecord()
    {
      Provider.CloneRepository(this);
    }

  }

}
