using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http.Headers;
using Illallangi.GitHubPS.Config;
using Illallangi.GitHubPS.Extensions;
using Octokit;

namespace Illallangi.GitHubPS.GitHub
{
    [Cmdlet(VerbsCommon.Get, Nouns.Abstract)]
    public abstract class GitHubPSCmdlet : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "UserName")]
        public string UserName { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, ParameterSetName = "UserName")]
        public string Token { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrEmpty(this.Token))
            {
                try
                {
                    var token = GitHubTokenCache.FromFile().Single(t => t.UserName.Equals(this.UserName));
                    this.Token = token.Token;
                }
                catch (Exception failure)
                {
                    this.WriteError(new ErrorRecord(
                        failure,
                        failure.Message,
                        ErrorCategory.InvalidResult,
                        GitHubConfig.Config));
                    return;
                }
            }
            try
            {
                var client = new GitHubClient(new ProductHeaderValue(GitHubConfig.Config.ClientId));
                client.Connection.Credentials = new Credentials(this.UserName, this.Token);

                this.WriteObject(this.Process(client), true);
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
                return;
            }
        }

        protected abstract IEnumerable<Object> Process(IGitHubClient client);
    }
}