namespace HealthcareManagerUiWebAssem.Models;

public class AppointmentManagementModel
{
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime Appointment { get; set; }
}