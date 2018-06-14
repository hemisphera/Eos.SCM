namespace Eos.SCM
{

  public class ResetScmFilesArgs : ScmCommandArgsBase, IResetScmFilesArgs
  {

    public string[] Files { get; set; }

  }

}
