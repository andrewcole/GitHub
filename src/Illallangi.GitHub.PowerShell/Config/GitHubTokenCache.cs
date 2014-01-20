using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Illallangi.GitHub.PowerShell.Extensions;
using Newtonsoft.Json;

namespace Illallangi.GitHub.PowerShell.Config
{
    public sealed class GitHubTokenCache : List<GitHubToken>
    {
        public IEnumerable<GitHubToken> AddToken(string userName, string token)
        {
            if (1 == this.Count(t => t.UserName.Equals(userName)))
            {
                this.Single(t => t.UserName.Equals(userName)).Token = token;
            }
            else
            {
                this.Add(new GitHubToken { UserName = userName, Token = token });
            }

            yield return this.ToFile().Single(t => t.UserName.Equals(userName));
        }

        public GitHubTokenCache ToFile()
        {
            var fileName = Environment.ExpandEnvironmentVariables(GitHubConfig.Config.TokenCache);
            
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }

            File.WriteAllBytes(
                fileName, 
                GitHubConfig.Config.EncryptTokenCache ?
                    JsonConvert.SerializeObject(this).Encrypt(
                        GitHubConfig.Config.EncryptTokenCacheOptionalEntropy,
                        GitHubConfig.Config.EncryptTokenCacheUseMachineScope) :
                Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(this)));
            
            return this;
        }

        public static GitHubTokenCache FromFile()
        {
            var fileName = Environment.ExpandEnvironmentVariables(GitHubConfig.Config.TokenCache);
            
            return File.Exists(fileName) ?
                GitHubConfig.Config.EncryptTokenCache ?
                    JsonConvert.DeserializeObject<GitHubTokenCache>(
                        File.ReadAllBytes(fileName).Decrypt(
                            GitHubConfig.Config.EncryptTokenCacheOptionalEntropy,
                            GitHubConfig.Config.EncryptTokenCacheUseMachineScope)) :
                    JsonConvert.DeserializeObject<GitHubTokenCache>(File.ReadAllText(fileName)) :
                new GitHubTokenCache().ToFile();
        }
    }
}
