using System.Collections.Generic;

using Common.Logging;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.Model;

namespace Illallangi.GitHub.Clients
{
    public sealed class UserClient : RestClient
    {
        #region Fields

        private const string GetUserUri = "users/{user}";
        
        #endregion

        #region Constructors

        public UserClient(IGitHubConfig config, IRestCache restCache = null, ILog log = null)
            : this(config.BaseUrl, config.GetDefaultParameters(), restCache, log)
        {
        }

        public UserClient(
            string baseUrl,
            IEnumerable<KeyValuePair<string, string>> defaultParameters = null,
            IRestCache restCache = null,
            ILog log = null)
            : base(baseUrl, defaultParameters, restCache, log)
        {
        }

        #endregion

        #region Methods
        
        public GitHubUser GetUser(string user)
        {
            return this.GetObject<GitHubUser>(GetUserUri, new Dictionary<string, string> { { "user", user } });
        }

        #endregion
    }
}