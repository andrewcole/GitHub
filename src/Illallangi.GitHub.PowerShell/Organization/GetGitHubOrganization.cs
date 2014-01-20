using System;
using System.Collections.Generic;
using System.Management.Automation;

using Illallangi.GitHub.Clients;
using Illallangi.GitHub.Model;

namespace Illallangi.GitHub.PowerShell.Organization
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubOrganization)]
    public sealed class GetGitHubOrganization : GitHubCmdlet<OrganizationClient>
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "GitHubUser")]
        public GitHubUser GitHubUser { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "User")]
        public string User { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Organization")]
        public string Organization { get; set; }
        
        protected override IEnumerable<object> Process(OrganizationClient client)
        {
            switch (this.ParameterSetName)
            {
                case "GitHubUser":
                    foreach (var organization in client.GetOrganizations(this.GitHubUser))
                    {
                        yield return organization;
                    }
                    break;
                case "Organization":
                    yield return client.GetOrganization(this.Organization);
                    break;
                case "User":
                    foreach (var organization in client.GetOrganizations(this.Get<UserClient>().GetUser(this.User)))
                    {
                        yield return organization;
                    }
                    break;
                default:
                    throw new NotImplementedException(this.ParameterSetName);
            }
        }
    }
}
