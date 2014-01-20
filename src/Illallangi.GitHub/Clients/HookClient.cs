using System;
using System.Collections.Generic;
using System.Linq;

using Common.Logging;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.Model;

using Newtonsoft.Json.Linq;

namespace Illallangi.GitHub.Clients
{
    public class HookClient : RestClient
    {
        #region Fields

        private const string GetHookUrl = "repos/{owner}/{repo}/hooks";

        #endregion

        #region Constructors

        public HookClient(IGitHubConfig config, IRestCache restCache = null, ILog log = null)
            : this(config.BaseUrl, config.GetDefaultParameters(), restCache, log)
        {
        }

        public HookClient(
            string baseUrl,
            IEnumerable<KeyValuePair<string, string>> defaultParameters = null,
            IRestCache restCache = null,
            ILog log = null)
            : base(baseUrl, defaultParameters, restCache, log)
        {
        }

        #endregion

        #region Methods

        public IEnumerable<GitHubHook> GetHooks(string owner, string repo)
        {
            return JArray.Parse(this.GetContent(GetHookUrl, new Dictionary<string, string> { { "owner", owner }, { "repo", repo } }))
                .Select(hook => this.GetHook(new Uri(hook["url"].Value<string>())));
        }

        public IEnumerable<GitHubHook> GetHooks(GitHubRepo repo)
        {
            return this.GetHooks(new Uri(repo.HooksUrl));
        }

        public IEnumerable<GitHubHook> GetHooks(Uri uri)
        {
            return JArray.Parse(this.GetContent(uri))
                .Select(hook => this.GetHook(new Uri(hook["url"].Value<string>())));
        }

        public GitHubHook GetHook(Uri uri)
        {
            return this.GetObject<GitHubHook>(uri);
        }

        #endregion
    }
}