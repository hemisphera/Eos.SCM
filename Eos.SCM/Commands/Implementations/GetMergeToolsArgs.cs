namespace Eos.SCM
{

  public class GetMergeToolsArgs : ScmCommandArgsBase, IGetItemArgs<ScmMergeTool>
  {

    public string[] Key { get; set; }

    public ConfigurationScope Scope { get; set; }

  }

}
