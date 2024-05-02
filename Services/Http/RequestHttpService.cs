using System.Text;
using System.Text.Json;
using HealthcareManagerUiWebAssem.Models;
using HealthcareManagerUiWebAssem.Services.UserSessionInformation;
using Microsoft.Extensions.Options;

namespace HealthcareManagerUiWebAssem.Services.Http;

public class RequestHttpService : IRequestHttpService
{
    private readonly string _apiEndpoint;
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;
    private readonly IUserSessionInformation _userSessionInformation;

    public RequestHttpService(IHttpClientFactory httpClientFactory, IUserSessionInformation userSessionInformation,
        IOptions<ApplicationSettings> settings)
    {
        _httpClient = httpClientFactory.CreateClient();
        _apiKey = settings.Value.ApiKey ?? "NoKey";
        _apiEndpoint = settings.Value.ApiEndpoint;
        _userSessionInformation = userSessionInformation;
    }

    public async Task<BaseResponse> GetByIdAsync(string uri, Guid id, IDictionary<string, string>? headers = null)
    {
        headers ??= new Dictionary<string, string>();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiEndpoint}{uri}/{id}");
        AddHeaders(request, headers);

        var response = await _httpClient.SendAsync(request);
        Thread.Sleep(1000);
        return await HandleResponse(response, uri);
    }

    public async Task<BaseResponse> GetAsync(string uri, IDictionary<string, string>? headers = null)
    {
        headers ??= new Dictionary<string, string>();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiEndpoint}{uri}");
        AddHeaders(request, headers);
        var authHeader = request.Headers.GetValues("Authorization");
        Console.WriteLine(authHeader);

        var response = await _httpClient.SendAsync(request);
        Thread.Sleep(1000);
        return await HandleResponse(response, uri);
    }


    public async Task<BaseResponse> PostAsync(string uri, object data, IDictionary<string, string>? headers = null)
    {
        headers ??= new Dictionary<string, string>();
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiEndpoint}{uri}"); // Prepend _apiEndpoint
        AddHeaders(request, headers);
        request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        Thread.Sleep(1000);
        return await HandleResponse(response, uri);
    }

    public async Task<BaseResponse> PutAsync(string uri, object data, IDictionary<string, string>? headers = null)
    {
        headers ??= new Dictionary<string, string>();
        var request = new HttpRequestMessage(HttpMethod.Put, $"{_apiEndpoint}{uri}");
        AddHeaders(request, headers);
        request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        Thread.Sleep(1000);
        return await HandleResponse(response, uri);
    }

    public async Task<BaseResponse> DeleteAsync(string uri, Guid id, IDictionary<string, string>? headers = null)
    {
        headers ??= new Dictionary<string, string>();
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiEndpoint}{uri}/{id}");
        AddHeaders(request, headers);

        var response = await _httpClient.SendAsync(request);
        Thread.Sleep(1000);
        return await HandleResponse(response, uri);
    }
    
    public async Task<BaseResponse> DeleteByFilterAsync(string uri, object data, IDictionary<string, string>? headers = null)
    {
        headers ??= new Dictionary<string, string>();
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiEndpoint}{uri}");
        AddHeaders(request, headers);
        request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        Thread.Sleep(1000);
        return await HandleResponse(response, uri);
    }

    private async void AddHeaders(HttpRequestMessage request, IDictionary<string, string>? headers)
    {
        request.Headers.Add("ApiKey", _apiKey);
        request.Headers.Add("Authorization", "Bearer " + await _userSessionInformation.GetTokenAsync());

        if (headers == null) return;
        foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
    }

    private async Task<BaseResponse> HandleResponse(HttpResponseMessage? response, string uri)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<JsonResponse>(
            responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        var baseResponse = new BaseResponse();
    
        if (responseJson != null)
        {
            baseResponse.StatusCode = responseJson.StatusCode;
            baseResponse.Message = responseJson.Message;
            baseResponse.Data = responseJson.Data.ToString();
            baseResponse.Headers = new Dictionary<string, string>();
        }
        
        foreach (var header in response.Headers.Concat(response.Content.Headers))
        {
            Console.WriteLine(header.Key);
            if (baseResponse.Headers.ContainsKey(header.Key))
            {
                baseResponse.Headers[header.Key] += ", " + string.Join(", ", header.Value);
            }
            else
            {
                baseResponse.Headers.Add(header.Key, string.Join(", ", header.Value));
            }
        }

        return baseResponse;
    }

    class JsonResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public JsonElement Data { get; set; }
    }
}
