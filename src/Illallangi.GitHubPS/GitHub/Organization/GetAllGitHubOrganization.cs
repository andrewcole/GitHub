using System;
using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHubPS.GitHub.Organization
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubOrganization)]
    public class GetGitHubOrganization : GitHubCmdlet<IOrganizationsClient>
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "GetAll")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "GetAllForCurrent")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "GetOrganization")]
        public override string UserName { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false, ParameterSetName = "GetAll")]
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false, ParameterSetName = "GetAllForCurrent")]
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false, ParameterSetName = "GetOrganization")]
        public override string Token { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "GetAll")]
        public string User { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "GetOrganization")]
        public string Organization { get; set; }

        protected override IEnumerable<object> Process(IOrganizationsClient client)
        {
            switch (this.ParameterSetName)
            {       
                case "GetAll":
                    foreach (var organization in client.GetAll(this.User).Result)
                    {
                        yield return organization;
                    }
                    break;

                case "GetAllForCurrent":
                    foreach (var organization in client.GetAllForCurrent().Result)
                    {
                        yield return organization;
                    }
                    break;

                case "GetOrganization":
                    yield return client.Get(this.Organization).Result;
                    break;

                default:
                    throw new NotImplementedException(this.ParameterSetName);
            }
        }

        
    }
}