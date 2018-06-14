namespace Eos.SCM
{

  public class GetRemotesArgs : ScmCommandArgsBase, IGetRemotesArgs
  {

    public string[] Key { get; set; }
    
    public ConfigurationScope Scope { get; set; }

  }

}