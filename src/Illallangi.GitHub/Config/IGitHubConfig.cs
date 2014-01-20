using System.Collections.Generic;

namespace Illallangi.GitHub.Config
{
    public interface IGitHubConfig
    {
        string BaseUrl { get; }

        IEnumerable<KeyValuePair<string, string>> GetDefaultParameters();
    }
}