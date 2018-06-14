using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Remove, ResourceNames.MergeTool)]
  public class RemoveMergeToolCmdlet : ScmCmdletBase, IRemoveItemArgs<ScmMergeTool>
  {

    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public string[] Key { get; set; }

    [Parameter]
    public ConfigurationScope Scope { get; set; }


    protected override void ProcessRecord()
    {
      Provider.RemoveMergeTools(this);
    }

  }

}
