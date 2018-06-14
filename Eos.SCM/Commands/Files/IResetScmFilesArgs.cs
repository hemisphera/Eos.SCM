namespace Eos.SCM
{

  public interface IResetScmFilesArgs : IScmCommandArgsBase
  {
    
    string[] Files { get; }

  }

}