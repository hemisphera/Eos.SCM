namespace Eos.SCM
{

  public interface ICheckoutChangesetArgs : IScmCommandArgsBase
  {
    
    bool Clean { get; }

    string TargetRev { get; }

    string NewBranchName { get; }

  }

}