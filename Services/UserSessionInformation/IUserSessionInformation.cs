namespace HealthcareManagerUiWebAssem.Services.UserSessionInformation;

public interface IUserSessionInformation
{
    Task SaveUserInformationAsync(Guid id, string role, string username, string email, string token);
        
    Task SetIdAsync(Guid id);
    Task SetRoleAsync(string role);
    Task SetUsernameAsync(string username);
    Task SetEmailAsync(string email);
    Task SetTokenAsync(string token);

    Task<Guid> GetIdAsync();
    Task<string> GetRoleAsync();
    Task<string> GetUsernameAsync();
    Task<string> GetEmailAsync();
    Task<string> GetTokenAsync();

    Task RefreshTokenAsync();
}