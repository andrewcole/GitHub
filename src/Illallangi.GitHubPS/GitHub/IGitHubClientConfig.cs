namespace Illallangi.GitHubPS.GitHub
{
    public interface IGitHubClientConfig
    {
        string UserName { get; }
        string Token { get; }
    }
}