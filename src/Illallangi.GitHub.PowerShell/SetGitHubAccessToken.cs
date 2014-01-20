using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

using Illallangi.GitHub.Config;

namespace Illallangi.GitHub.PowerShell
{
    [Cmdlet(VerbsCommon.Set, Nouns.GitHubAccessToken)]
    public sealed class SetGitHubAccessToken : NinjectCmdlet<GitHubModule>
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Token { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string UserName { get; set; }

        protected override void ProcessRecord()
        {
            var gitHubTokenCache = this.Get<ICollection<GitHubToken>>();

            if (1 == gitHubTokenCache.Count(token => token.UserName.Equals(this.UserName)))
            {
                gitHubTokenCache.Single(token => token.UserName.Equals(this.UserName)).Token = this.Token;
            }
            else
            {
                gitHubTokenCache.Add(new GitHubToken { UserName = this.UserName, Token = this.Token });
            }

            this.WriteObject(gitHubTokenCache.Single(token => token.UserName.Equals(this.UserName)));
        }
    }
}