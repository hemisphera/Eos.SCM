namespace Eos.SCM
{

  public class GetScmFileContentArgs : ScmCommandArgsBase, IGetScmFileContentArgs
  {

    public RevisionQuery Query { get; set; }

    public string File { get; set; }

  }

}