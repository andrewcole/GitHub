using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHubPS.GitHub.Release
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubRelease)]
    public class GetGitHubRelease : GitHubCmdlet<IReleasesClient>
    {
        protected override IEnumerable<object> Process(IReleasesClient client)
        {
            // TODO: Implement GetGitHubRelease
            // Task<IReadOnlyList<Release>> GetAll(string owner, string name);
            throw new System.NotImplementedException();
        }
    }
}