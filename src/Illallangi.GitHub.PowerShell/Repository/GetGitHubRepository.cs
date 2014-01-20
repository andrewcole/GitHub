using System;
using System.Collections.Generic;
using System.Management.Automation;

using Illallangi.GitHub.Clients;
using Illallangi.GitHub.Model;

namespace Illallangi.GitHub.PowerShell.Repository
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubRepository, DefaultParameterSetName = "OwnerRepo")]
    public sealed class GetGitHubRepository : GitHubCmdlet<RepoClient>
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "GitHubUser")]
        public GitHubUser GitHubUser { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "GitHubOrganization")]
        public GitHubOrganization GitHubOrganization { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "User")]
        public string User { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Organization")]
        public string Organization { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "OwnerRepo")]
        public string Owner { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "OwnerRepo")]
        public string Repo { get; set; }

        protected override IEnumerable<object> Process(RepoClient client)
        {
            switch (this.ParameterSetName)
            {
                case "OwnerRepo":
                    yield return client.GetRepo(this.Owner, this.Repo);
                    break;
                case "GitHubUser":
                    foreach (var repo in client.GetRepos(this.GitHubUser))
                    {
                        yield return repo;
                    }
                    break;
                case "GitHubOrganization":
                    foreach (var repo in client.GetRepos(this.GitHubOrganization))
                    {
                        yield return repo;
                    }
                    break;
                case "User":
                    foreach (var repo in client.GetRepos(this.Get<UserClient>().GetUser(this.User)))
                    {
                        yield return repo;
                    }
                    break;
                case "Organization":
                    foreach (var repo in client.GetRepos(this.Get<OrganizationClient>().GetOrganization(this.Organization)))
                    {
                        yield return repo;
                    }
                    break;
                default:
                    throw new NotImplementedException(this.ParameterSetName);
            }
        }
    }
}
