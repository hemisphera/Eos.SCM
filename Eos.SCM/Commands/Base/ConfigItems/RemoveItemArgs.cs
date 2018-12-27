namespace Eos.SCM
{
  
  public class RemoveItemArgs<T> : ScmCommandArgsBase, IRemoveItemArgs<T>
  {

    public ConfigurationScope Scope { get; set; }
    
    public string[] Key { get; set; }

  }

}
