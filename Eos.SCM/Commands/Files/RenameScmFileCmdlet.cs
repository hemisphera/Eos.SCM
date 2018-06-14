using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Rename, ResourceNames.File)]
  public class RenameScmFileCmdlet : ScmCmdletBase, IRenameScmFileArgs
  {

    [Parameter(Mandatory = true, Position = 0)]
    public string Filename { get; set; }

    [Parameter(Mandatory = true, Position = 1)]
    public string NewFilename { get; set; }

    [Parameter]
    public SwitchParameter MarkOnly { get; set; }


    protected override void ProcessRecord()
    {
      Provider.RenameScmFile(this);
    }
  }

}
