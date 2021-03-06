﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management.Automation;
using Microsoft.PowerShell.Commands;

using LibGit2Sharp;

using GitPowerShell.Parameters;

namespace GitPowerShell.Commands
{
    [Cmdlet(VerbsData.Initialize, "GitRepository")]
    [OutputType(typeof(Repository))]
    public class InitializeGitRepositoryCommand : PSCmdlet
    {
        [Parameter(Mandatory=false, ValueFromPipeline=true, Position=0, HelpMessage="The directory that will contain the git repository.  It will be created if it does not exist."), PathTransformation]
        public String Directory
        {
            get;
            set;
        }

        [Parameter(HelpMessage="If set, the directory will be a \"bare\" git repository.")]
        public SwitchParameter Bare
        {
            get;
            set;
        }

        [Parameter(HelpMessage = "If set, the created Repository will be returned.")]
        public SwitchParameter PassThru
        {
            get;
            set;
        }

        protected override void ProcessRecord()
        {
            /* Get an absolute directory based on the powershell current working directory. */
            String repositoryPath = Repository.Init(Directory != null ?
                Directory :
                SessionState.Path.CurrentFileSystemLocation.Path, Bare);

            if(PassThru)
            {
                WriteObject(new Repository(repositoryPath));
            }
        }
    }
}
