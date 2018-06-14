namespace Eos.SCM
{

  public interface IGetDiffArgs : IScmCommandArgsBase
  {
  
    RevisionQuery Query { get; }

    string[] Files { get; }

  }

}