namespace Eos.SCM
{

  public interface IDownloadChangesetArgs : IRemoteScmCommandArgsBase
  {

    /// <summary>
    /// Specifies whether the pull only checks what would be pulled or effectively pulls.
    /// </summary>
    bool CheckOnly { get; }

    /// <summary>
    /// The source URI or alias to pull from.
    /// </summary>
    string Source { get; }

    /// <summary>
    /// A revision query which indicates the changesets that should be pulled.
    /// </summary>
    string TargetRev { get; }

  }
  
}