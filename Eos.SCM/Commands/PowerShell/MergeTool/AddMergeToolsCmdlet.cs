using System.Management.Automation;

namespace Eos.SCM
{


  [Cmdlet(VerbsCommon.Add, ResourceNames.MergeTool)]
  public class AddMergeToolsCmdlet : ScmCmdletBase, IAddItemsArgs<ScmMergeTool>
  {

    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    public ScmMergeTool[] Items { get; set; }

    [Parameter]
    public ConfigurationScope Scope { get; set; }


    protected override void ProcessRecord()
    {
      Provider.AddMergeTools(this);
    }

  }

}
