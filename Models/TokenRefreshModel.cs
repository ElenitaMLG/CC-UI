namespace HealthcareManagerUiWebAssem.Models;

public class TokenRefreshModel
{
    public TokenRefreshModel(string token)
    {
        Token = token;
    }

    public string Token { get; private set; }
}