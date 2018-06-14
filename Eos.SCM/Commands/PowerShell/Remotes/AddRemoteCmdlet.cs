using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Add, ResourceNames.Remote)]
  public class AddRemoteCmdlet : ScmCmdletBase, IAddItemsArgs<ScmRemote>
  {

    [Parameter(Mandatory = true, Position = 0)]
    public ScmRemote[] Items { get; set; }

    [Parameter]
    public ConfigurationScope Scope { get; set; }


    protected override void ProcessRecord()
    {
      Provider.AddRemote(this);
    }

  }

}
