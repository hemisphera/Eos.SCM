using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Get, ResourceNames.Remote)]
  public class GetRemoteCmdlet : ScmCmdletBase, IGetItemArgs<ScmRemote>
  {

    [Parameter]
    public string[] Key { get; set; }

    [Parameter]
    public ConfigurationScope Scope { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.GetRemotes(this), true);
    }

  }

}
