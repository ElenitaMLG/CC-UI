using System.Text.Json;
using HealthcareManagerUiWebAssem.Models;
using HealthcareManagerUiWebAssem.Services.Profile;

namespace HealthcareManagerUiWebAssem.Services.User;

public class UserService
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid id)
    {
        UserId = id;
    }

    public async Task<UserUpdateProfileModel?> InitializeProfile(string role, IProfileService profileService)
    {
        var response = role switch
        {
            "doctor" => await profileService.GetDoctorById(UserId),
            "patient" => await profileService.GetPatientById(UserId),
            _ => null
        };
        if (response.Data != null)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (role.Equals("doctor", StringComparison.OrdinalIgnoreCase))
            {
                var doctor = JsonSerializer.Deserialize<Doctor>(response.Data.ToString(), jsonOptions);
                return new UserUpdateProfileModel
                {
                    Id = doctor.Id,
                    Name = doctor.Name,
                    Email = doctor.Email,
                    Password = doctor.Password,
                    Description = doctor.Description
                };
            }

            if (role.Equals("patient", StringComparison.OrdinalIgnoreCase))
            {
                var patient = JsonSerializer.Deserialize<Patient>(response.Data.ToString(), jsonOptions);
                return new UserUpdateProfileModel
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Email = patient.Email,
                    Password = patient.Password
                };
            }
        }

        return new UserUpdateProfileModel();
    }
}