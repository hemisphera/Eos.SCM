namespace Eos.SCM
{

  public class RevertScmFilesArgs : ScmCommandArgsBase, IRevertScmFilesArgs
  {

    public string[] Files { get; set; }

  }

}
