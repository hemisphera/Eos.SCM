namespace Eos.SCM
{

  public interface ICloneRepositoryArgs : IRemoteScmCommandArgsBase
  {
    
    string Source { get; }

  }

}