using System.Management.Automation;

namespace Illallangi.GitHubPS.GitHub
{
    public class GetGitHubUser : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            // var github = new GitHubClient(new ProductHeaderValue("Illallangi.GitHubPS"));
            // github.Connection.Credentials = this.Credentials.ConvertToOctokitCredentials();
            // this.WriteObject(github.Organization.GetAll(this.Credentials.UserName).Result, true);
        }
    }
}
