using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eos.SCM
{

  public class ScmFile
  {

    public string RepositoryPath { get; }

    public string Filename { get; }

    public string FullFilename => Path.Combine(RepositoryPath, Filename);

    public ScmFileStatus Status { get; }

    public bool IsDeleted => Status == ScmFileStatus.Missing || Status == ScmFileStatus.Removed;

    public bool IsModifiedOrNew => Status == ScmFileStatus.Modified || Status == ScmFileStatus.Added;


    internal ScmFile(string repositoryPath, string filename, ScmFileStatus status)
    {
      RepositoryPath = repositoryPath;
      Filename = filename;
      Status = status;
    }

  }

}
