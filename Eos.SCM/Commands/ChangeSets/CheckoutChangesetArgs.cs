namespace Eos.SCM
{

  public class CheckoutChangesetArgs : ScmCommandArgsBase, ICheckoutChangesetArgs
  {

    public bool Clean { get; set; }

    public RevisionQuery TargetRevision { get; set; }
    
    public string NewBranchName { get; set; }

  }

}