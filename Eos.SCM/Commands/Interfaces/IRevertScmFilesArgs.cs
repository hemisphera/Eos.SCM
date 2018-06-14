namespace Eos.SCM
{

  public interface IRevertScmFilesArgs : IScmCommandArgsBase
  {
    
    string[] Files { get; }

  }

}