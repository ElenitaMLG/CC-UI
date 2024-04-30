using HealthcareManagerUiWebAssem.Models;
using HealthcareManagerUiWebAssem.Services.Http;

namespace HealthcareManagerUiWebAssem.Services.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly IRequestHttpService _requestHttpService;

    public AppointmentService(IRequestHttpService requestHttpService)
    {
        _requestHttpService = requestHttpService;
    }

    public async Task<BaseResponse> GetAppointmentsByPatient(Guid patientId)
    {
        try
        {
            var response = await _requestHttpService.GetByIdAsync("/Appointments/patient", patientId);
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
    
    public async Task<BaseResponse> GetAppointmentsByDoctor(Guid doctorId)
    {
        try
        {
            var response = await _requestHttpService.GetByIdAsync("/Appointments/doctor", doctorId);
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


    public async Task<BaseResponse> CreateAppointment(
        AppointmentManagementModel model)
    {
        try
        {
            var response = await _requestHttpService.PostAsync("/Appointments", model);
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

    public async Task<BaseResponse> DeleteAppointment(AppointmentManagementModel model)
    {
        try
        {
            var response = await _requestHttpService.DeleteByFilterAsync("/Appointments", model);
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