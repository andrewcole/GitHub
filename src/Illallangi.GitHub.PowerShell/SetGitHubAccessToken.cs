using System.Management.Automation;
using Illallangi.GitHub.PowerShell.Config;

namespace Illallangi.GitHub.PowerShell
{
    [Cmdlet(VerbsCommon.Set, Nouns.GitHubAccessToken)]
    public sealed class SetGitHubAccessToken : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Token { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string UserName { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteObject(
                GitHubTokenCache
                    .FromFile()
                    .AddToken(this.UserName, this.Token));
        }
    }
}