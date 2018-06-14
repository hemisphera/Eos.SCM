using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Get, ResourceNames.FileContent)]
  public class GetScmFileContentCmdlet : ScmCmdletBase, IGetScmFileContentArgs
  {

    [Parameter]
    public RevisionQuery Query { get; set; }

    [Parameter]
    public string File { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.GetFileContents(this));
    }

  }

}