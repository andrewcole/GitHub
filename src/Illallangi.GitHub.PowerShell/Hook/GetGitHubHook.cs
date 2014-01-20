using System;
using System.Collections.Generic;
using System.Management.Automation;

using Illallangi.GitHub.Clients;
using Illallangi.GitHub.Model;

namespace Illallangi.GitHub.PowerShell.Hook
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubHook)]
    public sealed class GetGitHubHook : GitHubCmdlet<HookClient>
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "GitHubRepository")]
        public GitHubRepo GitHubRepo { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "OwnerRepo")]
        public string Owner { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "OwnerRepo")]
        public string Repo { get; set; }

        protected override IEnumerable<object> Process(HookClient client)
        {
            switch (this.ParameterSetName)
            {
                case "GitHubRepository":
                    foreach (var hook in client.GetHooks(this.GitHubRepo))
                    {
                        yield return hook;
                    }
                    break;
                case "OwnerRepo":
                    foreach (var hook in client.GetHooks(this.Owner, this.Repo))
                    {
                        yield return hook;
                    }
                    break;
                default:
                    throw new NotImplementedException(this.ParameterSetName);
            }
        }
    }
}
