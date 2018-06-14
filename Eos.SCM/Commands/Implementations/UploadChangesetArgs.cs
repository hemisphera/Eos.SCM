namespace Eos.SCM
{

  public class UploadChangesetArgs : RemoteScmCommandArgsBase, IUploadChangesetArgs
  {

    public bool CheckOnly { get; set; }

    public string Destination { get; set; }

    public RevisionQuery Query { get; set; }

  }

}
