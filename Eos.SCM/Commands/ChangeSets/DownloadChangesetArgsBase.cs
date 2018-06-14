namespace Eos.SCM
{

  public class DownloadChangesetArgs : RemoteScmCommandArgsBase, IDownloadChangesetArgs
  {

    public bool CheckOnly { get; set; }

    public bool ReturnChangesets { get; set; }

    public string Source { get; set; }

    public RevisionQuery Query { get; set; }

  }

}
