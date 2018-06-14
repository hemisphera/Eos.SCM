using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Get, ResourceNames.MergeTool)]
  public class GetMergeToolsCmdlet : ScmCmdletBase, IGetItemArgs<ScmMergeTool>
  {

    [Parameter]
    public string[] Key { get; }
    
    [Parameter]
    public ConfigurationScope Scope { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.GetMergeTools(this), true);
    }

  }

}
