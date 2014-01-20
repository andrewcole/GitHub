using System;
using System.Collections.Generic;
using System.Linq;

using Common.Logging;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.Model;

using Newtonsoft.Json.Linq;

namespace Illallangi.GitHub.Clients
{
    public class RepoClient : RestClient
    {
        #region Fields

        private const string GetRepoUrl = "repos/{owner}/{repo}";

        #endregion

        #region Constructors

        public RepoClient(IGitHubConfig config, IRestCache restCache = null, ILog log = null)
            : this(config.BaseUrl, config.GetDefaultParameters(), restCache, log)
        {
        }

        public RepoClient(
            string baseUrl,
            IEnumerable<KeyValuePair<string, string>> defaultParameters = null,
            IRestCache restCache = null,
            ILog log = null)
            : base(baseUrl, defaultParameters, restCache, log)
        {
        }

        #endregion

        #region Methods
        
        public GitHubRepo GetRepo(string owner, string repo)
        {
            return this.GetObject<GitHubRepo>(GetRepoUrl, new Dictionary<string, string> { { "owner", owner }, {"repo", repo } });
        }

        public IEnumerable<GitHubRepo> GetRepos(GitHubOwner owner)
        {
            return this.GetRepos(new Uri(owner.ReposUrl));
        }

        public IEnumerable<GitHubRepo> GetRepos(Uri uri)
        {
            return JArray.Parse(this.GetContent(uri))
                         .Select(repo => this.GetRepo(new Uri(repo["url"].Value<string>())));
        }

        public GitHubRepo GetRepo(Uri uri)
        {
            return this.GetObject<GitHubRepo>(uri);
        }

        #endregion
    }
}