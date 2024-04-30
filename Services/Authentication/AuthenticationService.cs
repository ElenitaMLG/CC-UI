using HealthcareManagerUiWebAssem.Models;
using HealthcareManagerUiWebAssem.Services.Http;

namespace HealthcareManagerUiWebAssem.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRequestHttpService _requestHttpService;

    public AuthenticationService(IRequestHttpService requestHttpService)
    {
        _requestHttpService = requestHttpService;
    }

    public async Task<BaseResponse> Login(UserLoginModel userLoginModel)
    {
        try
        {
            var response = await _requestHttpService.PostAsync("/Authorization/login", userLoginModel);
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

    public async Task<BaseResponse> RegisterDoctor(UserRegisterModel userRegistrationModel)
    {
        try
        {
            var response = await _requestHttpService.PostAsync("/Doctors/register", userRegistrationModel);
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
    
    public async Task<BaseResponse> RegisterPatient(UserRegisterModel userRegistrationModel)
    {
        try
        {
            var response = await _requestHttpService.PostAsync("/Patients/register", userRegistrationModel);
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

    public async Task<BaseResponse> ResetPassword(UserLoginModel dto)
    {
        try
        {
            var response = await _requestHttpService.PostAsync("/Authorization/reset_password", dto);
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

    public async Task<BaseResponse> RefreshToken(TokenRefreshModel dto)
    {
        try
        {
            var response = await _requestHttpService.PostAsync("/Authorization/refresh_token", dto);
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