using System.Linq;
using System.Net.Http.Headers;
using Illallangi.GitHub.PowerShell.Config;
using Ninject;
using Ninject.Modules;
using Octokit;

namespace Illallangi.GitHub.PowerShell
{
    public sealed class GitHubModule : NinjectModule
    {
        private readonly IGitHubClientConfig currentConfig;

        public GitHubModule(IGitHubClientConfig config)
        {
            this.currentConfig = config;
        }

        public override void Load()
        {
            this.Bind<ProductHeaderValue>().ToMethod(
                cx => new ProductHeaderValue(GitHubConfig.Config.ClientId));

            this.Bind<IGitHubClient>().ToMethod(
                cx => 
                {
                          var client = new GitHubClient(cx.Kernel.Get<ProductHeaderValue>());
                          if (!string.IsNullOrEmpty(this.Config.UserName))
                          {
                              client.Connection.Credentials = new Credentials(
                                  this.Config.UserName,
                                  string.IsNullOrEmpty(this.Config.Token)
                                      ? GitHubTokenCache.FromFile().Single(t => t.UserName.Equals(this.Config.UserName)).Token
                                      : this.Config.Token);
                          }
                          return client;
                });
        }

        public IGitHubClientConfig Config
        {
            get
            {
                return this.currentConfig;
            }
        }
    }
}