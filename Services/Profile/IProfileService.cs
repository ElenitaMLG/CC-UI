using HealthcareManagerUiWebAssem.Models;

namespace HealthcareManagerUiWebAssem.Services.Profile;

public interface IProfileService
{
    Task<BaseResponse> GetDoctors();

    Task<BaseResponse> GetPatients();

    Task<BaseResponse> GetDoctorById(Guid id);

    Task<BaseResponse> GetPatientById(Guid id);

    Task<BaseResponse> UpdateDoctorProfile(UserUpdateProfileModel userUpdateProfileModel);

    Task<BaseResponse> UpdatePatientProfile(UserUpdateProfileModel userUpdateProfileModel);

    Task<BaseResponse> DeleteDoctorProfile(Guid id);

    Task<BaseResponse> DeletePatientProfile(Guid id);
}