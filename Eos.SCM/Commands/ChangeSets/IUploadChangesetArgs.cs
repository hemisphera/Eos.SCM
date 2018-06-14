namespace Eos.SCM
{

  public interface IUploadChangesetArgs : IRemoteScmCommandArgsBase
  {

    bool CheckOnly { get; }

    string Destination { get; }

    RevisionQuery Query { get; }

  }

}