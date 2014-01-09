using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHub.GitHub.Repository
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubRepository)]
    public class GetGitHubRepository : GitHubCmdlet<IRepositoriesClient>
    {
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string User { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Organization { get; set; }

        protected override IEnumerable<object> Process(IRepositoriesClient client)
        {
            return !string.IsNullOrEmpty(this.Organization) ? 
                client.GetAllForOrg(this.Organization).Result : 
                client.GetAllForUser(this.User ?? this.UserName).Result;
        }
    }
}