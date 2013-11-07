using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHubPS.GitHub.Release
{
    [Cmdlet(VerbsCommon.New, Nouns.GitHubRelease)]
    public class NewGitHubRelease : GitHubCmdlet<IReleasesClient>
    {
        protected override IEnumerable<object> Process(IReleasesClient client)
        {
            // TODO: Implement NewGitHubRelease
            // Task<Release> CreateRelease(string owner, string name, ReleaseUpdate data);
            throw new System.NotImplementedException();
        }
    }
}