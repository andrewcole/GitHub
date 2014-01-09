using System.Collections.Generic;
using System.Management.Automation;
using Octokit;

namespace Illallangi.GitHub.GitHub.Release
{
    [Cmdlet(VerbsCommon.New, Nouns.GitHubReleaseAsset)]
    public class NewGitHubReleaseAsset : GitHubCmdlet<IReleasesClient>
    {
        protected override IEnumerable<object> Process(IReleasesClient client)
        {
            // TODO: Implement NewGitHubReleaseAsset
            // Task<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data);
            throw new System.NotImplementedException();
        }
    }
}