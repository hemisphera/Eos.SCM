namespace Eos.SCM
{
  
  public class AddItemsArgs<T> : ScmCommandArgsBase, IAddItemsArgs<T>
  {
    
    public ConfigurationScope Scope { get; set; }
    
    public T[] Items { get; set; }

  }

}
