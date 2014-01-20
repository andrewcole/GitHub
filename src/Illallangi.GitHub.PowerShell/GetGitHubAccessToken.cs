using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http.Headers;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.PowerShell.Config;
using Illallangi.GitHub.PowerShell.Extensions;
using Octokit;

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
                    var client = new GitHubClient(new ProductHeaderValue(this.Get<IGitHubConfig>().ClientId));
                    client.Connection.Credentials = this.Credentials.ConvertToOctokitCredentials();

                    var authToken =
                        client.Authorization.GetOrCreateApplicationAuthentication(
                            this.Get<IGitHubConfig>().ClientId,
                            this.Get<IGitHubConfig>().ClientSecret, 
                            new NewAuthorization()).Result;

                    this.WriteObject(new
                    {
                        authToken.Token,
                        this.Credentials.UserName,
                    });
                    
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