using System.Configuration;
using System.Net.Http.Headers;
using System.Reflection;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.PowerShell.Config;

using Ninject.Modules;

namespace Illallangi.GitHub.PowerShell
{
    public sealed class GitHubModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IGitHubConfig>()
                .ToMethod(
                    cx =>
                    (GitHubConfig)
                    ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)
                        .GetSection("GitHubConfig"));
        }
    }
}