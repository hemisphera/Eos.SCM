namespace Eos.SCM
{

  public interface IGetItemArgs<T> : IScmCommandArgsBase, IConfigurationCommand
  {
    
    string[] Key { get; }

  }

}
