namespace HealthcareManagerUiWebAssem.Models;

public class UserUpdateProfileModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
}