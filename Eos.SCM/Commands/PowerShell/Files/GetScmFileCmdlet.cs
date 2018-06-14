using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Get, ResourceNames.File)]
  public class GetScmFileCmdlet : ScmCmdletBase, IGetScmFileArgs
  {

    [Parameter]
    public bool All { get; set; }

    [Parameter]
    public bool ChangedOnly { get; set; }

    [Parameter]
    public RevisionQuery Query { get; set; }

    [Parameter]
    public string[] Files { get; set; }


    public GetScmFileCmdlet()
    {
      All = false;
      ChangedOnly = true;
    }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.GetFiles(this), true);
    }

  }
}
