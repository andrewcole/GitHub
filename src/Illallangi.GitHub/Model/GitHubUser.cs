using Newtonsoft.Json;

namespace Illallangi.GitHub.Model
{
    public sealed class GitHubUser : GitHubOwner
    {
        // ReSharper disable UnusedMember.Global
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("blog")]
        public string Blog { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("hireable")]
        public bool Hireable { get; set; }

        [JsonProperty("bio")]
        public object Bio { get; set; }
        // ReSharper restore UnusedMember.Global
    }
}