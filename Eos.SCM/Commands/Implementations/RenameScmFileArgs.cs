using System.Management.Automation;

namespace Eos.SCM
{

  public class RenameScmFileArgs : ScmCommandArgsBase, IRenameScmFileArgs
  {

    public string Filename { get; set; }

    public string NewFilename { get; set; }

    public SwitchParameter MarkOnly { get; set; }

  }

}
