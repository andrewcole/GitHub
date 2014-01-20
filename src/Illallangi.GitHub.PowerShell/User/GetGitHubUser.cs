using System.Collections.Generic;

using Illallangi.GitHub.Clients;

namespace Illallangi.GitHub.PowerShell
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, Nouns.GitHubUser)]
    public sealed class GetGitHubUser : GitHubCmdlet<UserClient>
    {
        [Parameter(Mandatory = true)]
        public string User { get; set; }

        protected override IEnumerable<object> Process(UserClient client)
        {
            yield return this.Get<UserClient>().GetUser(this.User);
        }
    }
}
