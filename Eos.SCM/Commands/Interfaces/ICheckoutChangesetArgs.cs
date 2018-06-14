namespace Eos.SCM
{

  public interface ICheckoutChangesetArgs : IScmCommandArgsBase
  {
    
    bool Clean { get; }

    RevisionQuery TargetRevision { get; }

    string NewBranchName { get; }

  }

}