using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using Illallangi.GitHub.Config;

namespace Illallangi.GitHub.PowerShell.Config
{
    public sealed class GitHubConfig : ConfigurationSection, IGitHubConfig
    {
        [ConfigurationProperty("ClientId", IsRequired = true)]
        public string ClientId
        {
            get { return (string)this["ClientId"]; }
        }

        [ConfigurationProperty("ClientSecret", IsRequired = true)]
        public string ClientSecret
        {
            get { return (string)this["ClientSecret"]; }
        }

        [ConfigurationProperty("TokenCache", DefaultValue = "%localappdata%\\Illallangi Enterprises\\GitHub\\GitHubTokens.json", IsRequired = false)]
        public string TokenCache
        {
            get { return (string)this["TokenCache"]; }
        }

        [ConfigurationProperty("EncryptTokenCache", DefaultValue = true, IsRequired = false)]
        public bool EncryptTokenCache
        {
            get
            {
                var b = (bool?)this["EncryptTokenCache"];
                return !b.HasValue || b.Value;
            }
        }

        [ConfigurationProperty("EncryptTokenCacheScope", DefaultValue = "machine", IsRequired = false)]
        public string EncryptTokenCacheScope
        {
            get { return (string) this["EncryptTokenCacheScope"]; }
        }

        [ConfigurationProperty("EncryptTokenCacheEntropy", DefaultValue = "IllallangiEnterprises", IsRequired = false)]
        public string EncryptTokenCacheEntropy
        {
            get { return (string)this["EncryptTokenCacheEntropy"]; }
        }

        [ConfigurationProperty("BaseUrl", DefaultValue = @"https://api.github.com/", IsRequired = false)]
        public string BaseUrl
        {
            get
            {
                return (string)this["BaseUrl"];
            }
        }

        [ConfigurationProperty("UserAgent", DefaultValue = @"illallangi-ps/GitHub", IsRequired = false)]
        public string UserAgent
        {
            get
            {
                return (string)this["UserAgent"];
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetDefaultParameters()
        {
            yield break;
        }

        public byte[] EncryptTokenCacheOptionalEntropy
        {
            get
            {
                return Encoding.ASCII.GetBytes(this.EncryptTokenCacheEntropy);
            }
        }

        public bool EncryptTokenCacheUseMachineScope
        {
            get
            {
                return this.EncryptTokenCacheScope.Equals("machine", StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}