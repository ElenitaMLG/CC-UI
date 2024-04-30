using HealthcareManagerUiWebAssem.Models;

namespace HealthcareManagerUiWebAssem.Services.Http;

public interface IRequestHttpService
{
    Task<BaseResponse> GetAsync(string uri, IDictionary<string, string> headers = null);

    Task<BaseResponse> GetByIdAsync(string uri, Guid id, IDictionary<string, string> headers = null);
    Task<BaseResponse> PostAsync(string uri, object data, IDictionary<string, string> headers = null);

    Task<BaseResponse> PutAsync(string uri, object data, IDictionary<string, string> headers = null);

    Task<BaseResponse> DeleteAsync(string uri, Guid id, IDictionary<string, string> headers = null);

    Task<BaseResponse> DeleteByFilterAsync(string uri, object data, IDictionary<string, string> headers = null);
}