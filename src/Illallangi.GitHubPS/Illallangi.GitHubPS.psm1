Function Get-GitHubToken
{
	Begin
	{
		$c = Get-Credential
		Get-GitHubAccessToken -Credentials $c |
			Set-GitHubAccessToken
	}
}