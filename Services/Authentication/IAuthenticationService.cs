using HealthcareManagerUiWebAssem.Models;

namespace HealthcareManagerUiWebAssem.Services.Authentication;

public interface IAuthenticationService
{
    Task<BaseResponse> Login(UserLoginModel userLoginModel);
    Task<BaseResponse> RegisterPatient(UserRegisterModel userRegistrationModel);
    Task<BaseResponse> RegisterDoctor(UserRegisterModel userRegistrationModel);
    Task<BaseResponse> ResetPassword(UserLoginModel userResetPasswordModel);
    Task<BaseResponse> RefreshToken(TokenRefreshModel dto);
}