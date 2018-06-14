namespace Eos.SCM
{

  public interface IAddItemsArgs<T> : IScmCommandArgsBase, IConfigurationCommand
  {
    
    T[] Items { get; }

  }

}
