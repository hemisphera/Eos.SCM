namespace Eos.SCM
{

  public class RemoveMergeToolsArgs : ScmCommandArgsBase, IRemoveItemArgs<ScmMergeTool>
  {

    public string[] Key { get; set; }
    
    public ConfigurationScope Scope { get; set; }

  }

}
