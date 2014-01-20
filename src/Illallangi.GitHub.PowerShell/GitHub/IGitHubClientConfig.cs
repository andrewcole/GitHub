namespace Illallangi.GitHub.PowerShell.GitHub
{
    public interface IGitHubClientConfig
    {
        string UserName { get; }
        string Token { get; }
    }
}