using System.Management.Automation;

namespace Eos.SCM
{

  public interface IRenameScmFileArgs : IScmCommandArgsBase
  {

    string Filename { get; }

    string NewFilename { get; }

    SwitchParameter MarkOnly { get; }

  }

}
