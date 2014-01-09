using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHub.GitHub.Miscellaneous
{
    [Cmdlet(VerbsData.Convert, Nouns.RawMarkDown)]
    public class ConvertRawMarkDown : GitHubCmdlet<IMiscellaneousClient>
    {
        protected override IEnumerable<object> Process(IMiscellaneousClient client)
        {
            // TODO: Implement ConvertRawMarkDown
            // Task<string> RenderRawMarkdown(string markdown);
            throw new System.NotImplementedException();
        }
    }
}