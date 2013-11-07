using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHubPS.GitHub.Notification
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubNotification)]
    public class GetGitHubNotification : GitHubCmdlet<INotificationsClient>
    {
        protected override IEnumerable<object> Process(INotificationsClient client)
        {
            // TODO: Implement GetGitHubNotification
            // Task<IReadOnlyList<Notification>> GetAllForCurrent();
            // Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name);
            throw new System.NotImplementedException();
        }
    }
}