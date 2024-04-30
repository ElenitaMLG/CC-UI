using HealthcareManagerUiWebAssem.Models;
using HealthcareManagerUiWebAssem.Services.Http;

namespace HealthcareManagerUiWebAssem.Services.Profile;

public class ProfileService : IProfileService
{
    private readonly IRequestHttpService _requestHttpService;

    public ProfileService(IRequestHttpService requestHttpService)
    {
        _requestHttpService = requestHttpService;
    }

    public async Task<BaseResponse> GetDoctors()
    {
        try
        {
            var response = await _requestHttpService.GetAsync("/Doctors");
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }

    public async Task<BaseResponse> GetPatients()
    {
        try
        {
            var response = await _requestHttpService.GetAsync("/Patients");
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }

    public async Task<BaseResponse> GetDoctorById(Guid id)
    {
        try
        {
            var response = await _requestHttpService.GetByIdAsync("/Doctors", id);
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }

    public async Task<BaseResponse> GetPatientById(Guid id)
    {
        try
        {
            var response = await _requestHttpService.GetByIdAsync("/Patients", id);
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }


    public async Task<BaseResponse> UpdateDoctorProfile(
        UserUpdateProfileModel userUpdateProfileModel)
    {
        try
        {
            var response = await _requestHttpService.PutAsync("/Doctors", userUpdateProfileModel);
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }

    public async Task<BaseResponse> UpdatePatientProfile(
        UserUpdateProfileModel userUpdateProfileModel)
    {
        try
        {
            var response = await _requestHttpService.PutAsync("/Patients", userUpdateProfileModel);
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }

    public async Task<BaseResponse> DeleteDoctorProfile(Guid id)
    {
        try
        {
            var response = await _requestHttpService.DeleteAsync("/Doctors", id);
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }

    public async Task<BaseResponse> DeletePatientProfile(Guid id)
    {
        try
        {
            var response = await _requestHttpService.DeleteAsync("/Patients", id);
            return response;
        }
        catch (Exception ex)
        {
            return new BaseResponse
            {
                StatusCode = HttpStatusCodes.InternalServerError,
                Data = null,
                Message = ex.Message
            };
        }
    }
}