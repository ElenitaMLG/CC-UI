namespace HealthcareManagerUiWebAssem.Models;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Data { get; set; }
    public IDictionary<string, string>? Headers { get; set; } // New field to store headers
}
