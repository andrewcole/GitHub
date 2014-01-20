using System.Configuration;
using System.Net.Http.Headers;
using System.Reflection;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.PowerShell.Config;

using Ninject.Modules;
using Octokit;

namespace Illallangi.GitHub.PowerShell
{
    public sealed class GitHubModule : NinjectModule
    {
        public override void Load()
        {

            this.Bind<IGitHubClient>().ToMethod(cx => new GitHubClient(new ProductHeaderValue("Blah")));

            this.Bind<IGitHubConfig>()
                .ToMethod(
                    cx =>
                    (GitHubConfig)
                    ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)
                        .GetSection("GitHubConfig"));

        }
    }
}