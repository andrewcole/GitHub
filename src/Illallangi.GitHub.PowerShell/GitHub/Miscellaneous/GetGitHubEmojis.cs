using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHub.PowerShell.GitHub.Miscellaneous
{
    [Cmdlet(VerbsCommon.Get, Nouns.GitHubEmojis)]
    public class GetGitHubEmojis : GitHubCmdlet<IMiscellaneousClient>
    {
        protected override IEnumerable<object> Process(IMiscellaneousClient client)
        {
            // TODO: Implement GetGitHubEmojis
            // Task<IReadOnlyDictionary<string, System.Uri>> GetEmojis();
            throw new System.NotImplementedException();
        }
    }
}