using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using HealthcareManagerUiWebAssem.Models;
using Microsoft.Extensions.Options;

namespace HealthcareManagerUiWebAssem.Services.UserSessionInformation;

public class UserSessionInformation : IUserSessionInformation
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly string _apiEndpoint;
    private readonly string _apiKey;

    public UserSessionInformation(ILocalStorageService localStorageService, IHttpClientFactory httpClientFactory, IOptions<ApplicationSettings> settings)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClientFactory.CreateClient();
        _apiEndpoint = settings.Value.ApiEndpoint;
        _apiKey = settings.Value.ApiKey ?? "NoKey";
    }

    public async Task SaveUserInformationAsync(Guid id, string role, string username, string email, string token)
    {
        await SetIdAsync(id);
        await SetRoleAsync(role);
        await SetUsernameAsync(username);
        await SetEmailAsync(email);
        await SetTokenAsync(token);
    }
    
    public async Task SetIdAsync(Guid id)
    {
        await _localStorageService.SetItemAsync("id", id);
    }

    public async Task SetRoleAsync(string role)
    {
        await _localStorageService.SetItemAsync("role", role);
    }

    public async Task SetUsernameAsync(string username)
    {
        await _localStorageService.SetItemAsync("username", username);
    }

    public async Task SetEmailAsync(string email)
    {
        await _localStorageService.SetItemAsync("email", email);
    }

    public async Task SetTokenAsync(string token)
    {
        await _localStorageService.SetItemAsync("jwtToken", token);
    }
    
    public async Task<Guid> GetIdAsync()
    {
        return await _localStorageService.GetItemAsync<Guid>("id");
    }
    
    public async Task<string> GetRoleAsync()
    {
        return await _localStorageService.GetItemAsync<string>("role");
    }

    public async Task<string> GetUsernameAsync()
    {
        return await _localStorageService.GetItemAsync<string>("username");
    }

    public async Task<string> GetEmailAsync()
    {
        return await _localStorageService.GetItemAsync<string>("email");
    }

    public async Task<string> GetTokenAsync()
    {
        return await _localStorageService.GetItemAsync<string>("jwtToken");
    }

    public async Task RefreshTokenAsync()
    {
        var currentToken = await GetTokenAsync();
        if (string.IsNullOrWhiteSpace(currentToken)) return;

        var tokenRequestModel = new TokenRefreshModel(currentToken);
        var requestUri = $"{_apiEndpoint}/refresh_token";

        var jsonContent = JsonContent.Create(tokenRequestModel);
        var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = jsonContent
        };

        request.Headers.Add("ApiKey", _apiKey);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return;

        var responseContent = await response.Content.ReadAsStringAsync();
        var baseResponse = JsonSerializer.Deserialize<BaseResponse>(
            responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (baseResponse == null || baseResponse.StatusCode < HttpStatusCodes.BadRequest) return;

        var tokenObj = JsonSerializer.Deserialize<TokenRefreshModel>(baseResponse.Data.ToString(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (tokenObj != null) await SetTokenAsync(tokenObj.Token);
    }
}
