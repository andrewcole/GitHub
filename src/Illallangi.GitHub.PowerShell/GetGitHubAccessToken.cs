using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

using Illallangi.GitHub.Clients;
using Illallangi.GitHub.Config;

namespace Illallangi.GitHub.PowerShell
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubAccessToken, DefaultParameterSetName = "Cache")]
    public class GetGitHubAccessToken : NinjectCmdlet<GitHubModule>
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName="API")]
        public PSCredential Credentials { get; set; }

        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Cache")]
        public string UserName { get; set; }

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case "API":
                    this.WriteObject(
                        this.Get<AuthorizationsClient>()
                            .GetOrCreateAuthorization(string.Empty, new[] { string.Empty }, string.Empty, string.Empty));
                    break;
                case "Cache":
                    this.WriteObject(
                        this.Get<ICollection<GitHubToken>>()
                            .Where(token => string.IsNullOrWhiteSpace(this.UserName) || token.UserName.Equals(this.UserName)));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}