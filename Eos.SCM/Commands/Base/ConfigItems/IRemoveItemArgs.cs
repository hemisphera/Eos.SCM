namespace Eos.SCM
{

  public interface IRemoveItemArgs<T> : IScmCommandArgsBase, IConfigurationCommand
  {

    string[] Key { get; }

  }

}
