using Newtonsoft.Json;

namespace Illallangi.GitHub.Model
{
    public sealed class GitHubOrganization : GitHubOwner
    {
        // ReSharper disable UnusedMember.Global
        [JsonProperty("members_url")]
        public string MembersUrl { get; set; }

        [JsonProperty("public_members_url")]
        public string PublicMembersUrl { get; set; }
        // ReSharper restore UnusedMember.Global
    }
}