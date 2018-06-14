using System.Net;
using eos.SCM;

namespace Eos.SCM
{

  public interface IScmProvider
  {

    string Name { get; }

    string UserConfigFilename { get; }


    void Commit(ICommitScmFileArgs args);

    ScmFile[] GetFiles(IGetScmFileArgs args);

    void Revert(IRevertScmFilesArgs args);

    void RenameScmFile(IRenameScmFileArgs args);


    Changeset[] DownloadChangesets(IDownloadChangesetArgs args);

    Changeset[] UploadChangesets(IUploadChangesetArgs args);

    FileDiff[] GetDiff(IGetDiffArgs args);

    Changeset[] GetChangesets(IGetChangesetArgs args);

    void CheckoutChangeset(ICheckoutChangesetArgs args);

    byte[] GetFileContents(IGetScmFileContentArgs args);


    void CreateRepository(INewRepositoryArgs args);

    void CloneRepository(ICloneRepositoryArgs args);


    ScmRemote[] GetRemotes(IGetItemArgs<ScmRemote> args);

    void AddRemote(IAddItemsArgs<ScmRemote> args);

    void RemoveRemote(IRemoveItemArgs<ScmRemote> args);


    void CreateBranch(INewBranchArgs args);

    void RemoveBranch(IRemoveBranchArgs args);

    string[] GetBranches(IGetBranchesArgs args);


    ScmMergeTool[] GetMergeTools(IGetItemArgs<ScmMergeTool> args);

    void AddMergeTools(IAddItemsArgs<ScmMergeTool> args);

    void RemoveMergeTools(IRemoveItemArgs<ScmMergeTool> args);


    bool IsRepository(string repositoryPath);

    bool IsError(int exitCode);


    string FormatCredentials(ICredentials credentials);

    string FormatQuery(RevisionQuery query);

  }
}
