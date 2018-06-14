namespace Eos.SCM
{

  public class DownloadChangesetArgs : RemoteScmCommandArgsBase, IDownloadChangesetArgs
  {

    public bool CheckOnly { get; set; }

    public string Source { get; set; }

    public string TargetRev { get; set; }

  }

}
