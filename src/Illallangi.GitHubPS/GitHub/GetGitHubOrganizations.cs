using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHubPS.GitHub
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubOrganization)]
    public class GetGitHubOrganization : GitHubPSCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string User { get; set; }

        protected override IEnumerable<object> Process(IGitHubClient client)
        {
            return client.Organization.GetAll(this.User ?? this.UserName).Result;
        }
    }

    [Cmdlet(VerbsCommon.Get, Nouns.GitHubRepository)]
    public class GetGitHubRepository : GitHubPSCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string User { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Organization { get; set; }

        protected override IEnumerable<object> Process(IGitHubClient client)
        {
            return !string.IsNullOrEmpty(this.Organization) ? 
                client.Repository.GetAllForOrg(this.Organization).Result : 
                client.Repository.GetAllForUser(this.User ?? this.UserName).Result;
        }
    }


}