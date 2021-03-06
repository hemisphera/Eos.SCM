﻿using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet("Download", ResourceNames.ChangeSet)]
  public class DownloadChangesetCmdlet : RemoteScmCmdletBase, IDownloadChangesetArgs
  {

    [Parameter]
    public bool CheckOnly { get; set; }

    [Parameter(Mandatory = true, Position = 0)]
    public string Source { get; set; }

    [Parameter(Position = 1)]
    public string TargetRev { get; set; }


    protected override void ProcessRecord()
    {
      WriteObject(Provider.DownloadChangesets(this), true);
    }
  }

}