namespace Eos.SCM
{

  public class GetItemArgs<T> : ScmCommandArgsBase, IGetItemArgs<T>
  {

    public ConfigurationScope Scope { get; set; }
    
    public string[] Key { get; set; }

  }

}
