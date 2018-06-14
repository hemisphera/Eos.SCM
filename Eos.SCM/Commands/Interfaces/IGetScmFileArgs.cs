namespace Eos.SCM
{

  public interface IGetScmFileArgs : IScmCommandArgsBase
  {
    
    bool All { get; set; }

    string[] Files { get; set; }

    RevisionQuery Query { get; set; }

  }

}