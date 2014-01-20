using System.Collections.Generic;

namespace Illallangi.GitHub.Config
{
    public interface IGitHubConfig
    {
        string BaseUrl { get; }

        string ClientId { get; }

        string ClientSecret { get; }

        IEnumerable<KeyValuePair<string, string>> GetDefaultParameters();
    }
}