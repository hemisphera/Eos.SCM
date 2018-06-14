using System.Management.Automation;

namespace Eos.SCM
{

  public class RemoveRemoteCmdlet : ScmCmdletBase, IRemoveItemArgs<ScmRemote>
  {

    [Parameter]
    public string[] Key { get; set; }

    [Parameter]
    public ConfigurationScope Scope { get; set; }


    protected override void ProcessRecord()
    {
      Provider.RemoveRemote(this);
    }

  }

}
