﻿using System.Management.Automation;

namespace Eos.SCM
{

  [Cmdlet(VerbsCommon.Reset, ResourceNames.File)]
  public class ResetScmFilesCmdlet : ScmCmdletBase, IRevertScmFilesArgs
  {

    [Parameter(Mandatory = true)]
    public string[] Files { get; set; }


    protected override void ProcessRecord()
    {
      Provider.Revert(this);
    }

  }

}
