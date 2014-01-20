using System;
using System.Collections.Generic;
using System.Linq;

using Common.Logging;

using Illallangi.GitHub.Config;
using Illallangi.GitHub.Model;

using Newtonsoft.Json.Linq;

namespace Illallangi.GitHub.Clients
{
    public sealed class OrganizationClient : RestClient
    {
        #region Fields

        private const string GetOrganizationUrl = "orgs/{organization}";

        #endregion

        #region Constructors

        public OrganizationClient(IGitHubConfig config, IRestCache restCache = null, ILog log = null)
            : this(config.BaseUrl, config.GetDefaultParameters(), restCache, log)
        {
        }

        public OrganizationClient(
            string baseUrl,
            IEnumerable<KeyValuePair<string, string>> defaultParameters = null,
            IRestCache restCache = null,
            ILog log = null)
            : base(baseUrl, defaultParameters, restCache, log)
        {
        }

        #endregion

        #region Methods

        public IEnumerable<GitHubOrganization> GetOrganizations(GitHubUser user)
        {
            return this.GetOrganizations(new Uri(user.OrganizationsUrl));
        }

        public IEnumerable<GitHubOrganization> GetOrganizations(Uri uri)
        {
            return JArray.Parse(this.GetContent(uri))
                         .Select(org => this.GetOrganization(new Uri(org["url"].Value<string>())));
        }

        public GitHubOrganization GetOrganization(Uri uri)
        {
            return this.GetObject<GitHubOrganization>(uri);
        }

        public GitHubOrganization GetOrganization(string organization)
        {
            return this.GetObject<GitHubOrganization>(GetOrganizationUrl, new Dictionary<string, string> { { "organization", organization } });
        }

        #endregion
    }
}