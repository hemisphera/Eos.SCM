using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eos.SCM;
using Eos.SCM.Commands;

namespace Eos.SCM
{


  /// <summary>
  /// Represents a local repository that is accessible through the filesystem.
  /// </summary>
  public class Repository
  {

    public string Folder { get; }

    public IScmProvider Provider { get; }


    public Repository(string folder)
      : this(folder, null)
    {
    }

    public Repository(string folder, IScmProvider provider)
    {
      if (provider == null)
        provider = ScmProviderFactory.Instance.FindProviderByPath(folder);
      Provider = provider;
      Folder = folder;
    }


    /// <summary>
    /// Gets all changed files in from the working directory.
    /// </summary>
    /// <returns></returns>
    public ScmFile[] GetFiles()
    {
      return Provider.GetFiles(new GetScmFileArgs
      {
        RepositoryPath = Folder
      });
    }

    /// <summary>
    /// Gets all files modified in a given changeset
    /// </summary>
    /// <param name="hash">The changeset hash to check</param>
    /// <returns></returns>
    public ScmFile[] GetFiles(string hash)
    {
      return Provider.GetFiles(new GetScmFileArgs
      {
        RepositoryPath = Folder,
        Query = hash
      });
    }

    /// <summary>
    /// Gets all files modified in a given changeset
    /// </summary>
    /// <param name="fromHash"></param>
    /// <param name="toHash"></param>
    /// <returns></returns>
    public ScmFile[] GetFiles(string fromHash, string toHash)
    {
      return Provider.GetFiles(new GetScmFileArgs
      {
        RepositoryPath = Folder,
        Query = new RevisionQuery
        {
          FromRevisionId = fromHash,
          ToRevisionId = toHash
        }
      });
    }


    public Changeset Commit(string[] files, string message)
    {
      return Provider.Commit(new CommitScmFileArgs
      {
        RepositoryPath = Folder,
        Files = files,
        Message = message
      });
    }

    public Changeset Commit(string message)
    {
      return Provider.Commit(new CommitScmFileArgs
      {
        RepositoryPath = Folder,
        All = true,
        Message = message
      });
    }


    public Changeset GetCurrentChangeset()
    {
      return Provider.GetChangesets(new GetChangesetsArgs
      {
        RepositoryPath = Folder,
        Current = true
      }).LastOrDefault();
    }

    public Changeset GetChangeset(string hash)
    {
      return Provider.GetChangesets(new GetChangesetsArgs
      {
        RepositoryPath = Folder,
        Query = hash
      }).LastOrDefault();
    }

    public Changeset[] GetChangesets(string query)
    {
      return Provider.GetChangesets(new GetChangesetsArgs
      {
        RepositoryPath = Folder,
        Query = query
      });
    }

    public Changeset[] GetChangesets(string fromHash, string toHash)
    {
      return Provider.GetChangesets(new GetChangesetsArgs
      {
        RepositoryPath = Folder,
        Query = new RevisionQuery
        {
          FromRevisionId = fromHash, 
          ToRevisionId = toHash
        }
      });
    }


    public Changeset[] GetIncomingChangesets(string source, string targetRev)
    {
      return Provider.DownloadChangesets(new DownloadChangesetArgs
      {
        RepositoryPath = Folder,
        CheckOnly = true,
        Source = source,
        TargetRev = targetRev
      });
    }

    public Changeset[] GetOutgoingChangesets(string destination, string targetRev)
    {
      return Provider.UploadChangesets(new UploadChangesetArgs
      {
        RepositoryPath = Folder,
        CheckOnly = true,
        Destination = destination,
        TargetRev = targetRev
      });
    }


    public void Rename(string oldFilename, string newFilename, bool markOnly)
    {
      Provider.RenameScmFile(new RenameScmFileArgs
      {
        RepositoryPath = Folder,
        Filename = oldFilename,
        NewFilename = newFilename,
        MarkOnly = markOnly
      });
    }

    public void Revert(string file)
    {
      Revert(new[] { file });
    }

    public void Revert(string[] files)
    {
      Provider.Revert(new ResetScmFilesArgs
      {
        RepositoryPath = Folder,
        Files = files
      });
    }

    public byte[] SaveFileAtRevision(string filename, string hash)
    {
      return Provider.GetFileContents(new GetScmFileContentArgs
      {
        RepositoryPath = Folder,
        File = filename,
        Query = hash
      });
    }

    public FileDiff GetDiff(string filename, string hash1, string hash2)
    {
      return Provider.GetDiff(new GetDiffArgs
      {
        RepositoryPath = Folder,
        Files = new[] {filename},
        Query = new RevisionQuery
        {
          FromRevisionId = hash1,
          ToRevisionId = hash2
        }
      }).LastOrDefault();
    }


  }

}
