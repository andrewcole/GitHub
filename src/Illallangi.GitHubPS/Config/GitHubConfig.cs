using System;
using System.Configuration;
using System.Text;

namespace Illallangi.GitHubPS.Config
{
    public sealed class GitHubConfig : ConfigurationSection
    {
        private static string staticPath;
        private static Configuration staticExeConfig;
        private static GitHubConfig staticConfig;

        private static string Path
        {
            get
            {
                return GitHubConfig.staticPath ??
                    (GitHubConfig.staticPath = System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        private static Configuration ExeConfig
        {
            get
            {
                return GitHubConfig.staticExeConfig ??
                    (GitHubConfig.staticExeConfig = ConfigurationManager.OpenExeConfiguration(GitHubConfig.Path));
            }
        }

        public static GitHubConfig Config
        {
            get
            {
                return GitHubConfig.staticConfig ??
                    (GitHubConfig.staticConfig = (GitHubConfig)GitHubConfig.ExeConfig.GetSection("GitHubConfig"));
            }
        }

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

        [ConfigurationProperty("TokenCache", DefaultValue = "%localappdata%\\Illallangi Enterprises\\GitHubPS\\GitHubTokens.json", IsRequired = false)]
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