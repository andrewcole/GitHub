using System;
using System.Linq;
using System.Management.Automation;
using System.Net.Http.Headers;
using Illallangi.GitHubPS.Config;
using Illallangi.GitHubPS.Extensions;
using Octokit;

namespace Illallangi.GitHubPS.GitHub
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubAccessToken, DefaultParameterSetName = "Cache")]
    public class GetGitHubAccessToken : PSCmdlet
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
                    try
                    {
                        var client = new GitHubClient(new ProductHeaderValue(GitHubConfig.Config.ClientId));
                        client.Connection.Credentials = this.Credentials.ConvertToOctokitCredentials();

                        var authToken =
                            client.Authorization.GetOrCreateApplicationAuthentication(GitHubConfig.Config.ClientId,
                                GitHubConfig.Config.ClientSecret, new NewAuthorization()).Result;

                        this.WriteObject(new
                        {
                            authToken.Token,
                            this.Credentials.UserName,
                        });
                    }
                    catch (AggregateException failures)
                    {
                        foreach (var failure in failures.InnerExceptions)
                        {
                            this.WriteError(new ErrorRecord(
                                failure,
                                failure.Message,
                                ErrorCategory.InvalidResult,
                                GitHubConfig.Config));
                        }
                    }
                    catch (Exception failure)
                    {
                        this.WriteError(new ErrorRecord(
                            failure,
                            failure.Message,
                            ErrorCategory.InvalidResult,
                            GitHubConfig.Config));
                    }
                    break;
                case "Cache":
                    this.WriteObject(
                        GitHubTokenCache
                            .FromFile()
                            .Where(token => string.IsNullOrWhiteSpace(this.UserName) || token.UserName.Equals(this.UserName)));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}