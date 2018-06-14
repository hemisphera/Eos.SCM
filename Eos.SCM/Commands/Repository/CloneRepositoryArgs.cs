namespace Eos.SCM
{

  public class CloneRepositoryArgs : RemoteScmCommandArgsBase, ICloneRepositoryArgs
  {

    public string Source { get; set; }

  }

}