namespace Illallangi.GitHub.PowerShell
{
    public interface IGitHubClientConfig
    {
        string UserName { get; }
        string Token { get; }
    }
}