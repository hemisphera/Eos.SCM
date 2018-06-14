namespace Eos.SCM
{

  public class AddMergeToolsArgs : ScmCommandArgsBase, IAddItemsArgs<ScmMergeTool>
  {

    public ScmMergeTool[] Items { get; set; }

    public ConfigurationScope Scope { get; set; }

  }

}
