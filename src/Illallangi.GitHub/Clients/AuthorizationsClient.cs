using System.Collections.Generic;

using Common.Logging;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.Model;

namespace Illallangi.GitHub.Clients
{
    public sealed class AuthorizationsClient : RestClient
    {
        #region Fields

        private const string PostAuthorizationUri = "authorizations/clients/{client_id}";
        
        #endregion

        #region Constructors

        public AuthorizationsClient(IGitHubConfig config, IRestCache restCache = null, ILog log = null)
            : this(config.BaseUrl, config.GetDefaultParameters(), restCache, log)
        {
        }

        public AuthorizationsClient(
            string baseUrl,
            IEnumerable<KeyValuePair<string, string>> defaultParameters = null,
            IRestCache restCache = null,
            ILog log = null)
            : base(baseUrl, defaultParameters, restCache, log)
        {
        }

        #endregion

        #region Methods

        public GitHubAuthorization GetOrCreateAuthorization(
            string clientSecret,
            string[] scopes,
            string note,
            string noteUrl)
        {
            return this.GetObject<GitHubAuthorization>(PostAuthorizationUri, new Dictionary<string, string> { { "client_id", string.Empty } });
        }

        #endregion
    }
}