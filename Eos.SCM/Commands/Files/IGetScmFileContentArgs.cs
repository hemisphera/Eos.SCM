namespace Eos.SCM
{

  public interface IGetScmFileContentArgs : IScmCommandArgsBase
  {
    
    RevisionQuery Query { get; }

    string File { get; }

  }

}