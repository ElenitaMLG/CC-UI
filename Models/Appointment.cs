namespace HealthcareManagerUiWebAssem.Models;

public class Appointment
{
    public string Id { get; set; }
    public string PatientId { get; set; }
    public string DoctorId { get; set; }
    public List<DateTime> AppointmentsList { get; set; } = [];
}