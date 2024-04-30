using HealthcareManagerUiWebAssem.Models;

namespace HealthcareManagerUiWebAssem.Services.Appointment;

public interface IAppointmentService
{
    Task<BaseResponse> GetAppointmentsByPatient(Guid patientId);

    Task<BaseResponse> GetAppointmentsByDoctor(Guid doctorId);

    Task<BaseResponse> CreateAppointment(AppointmentManagementModel model);

    Task<BaseResponse> DeleteAppointment(AppointmentManagementModel model);
}