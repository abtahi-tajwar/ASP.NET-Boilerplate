using System.Text.Json;
using ASPBoilerplate.Configurations;

namespace ASPBoilerplate.Services;

[ScopedService]
public class GoogleAuthService {
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public GoogleAuthService(IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClient;
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<GoogleTokenResponse?> ExchangeCodeForToken(string code)
    {
        var _httpClient = _httpClientFactory.CreateClient();
        var request = _httpContextAccessor.HttpContext?.Request;
        var RedirectUri = $"{request.Scheme}://{request.Host}{GoogleAuthSettings.RedirectEndpoint}";

        if (GoogleAuthSettings.ClientId == null || GoogleAuthSettings.ClientSecret == null) {
            throw new Exception("Please provide google client ID and secret");
        }
        var values = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", GoogleAuthSettings.ClientId },
            { "client_secret", GoogleAuthSettings.ClientSecret },
            { "redirect_uri", RedirectUri },
            { "grant_type", "authorization_code" }
        };

        var content = new FormUrlEncodedContent(values);
        var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);
        var jsonResposne = await response.Content.ReadFromJsonAsync<object>();

        if (!response.IsSuccessStatusCode)
            return null;

        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<GoogleTokenResponse>(responseBody);
    }

    public async Task<GoogleUserInfo?> GetGoogleUserInfo(string accessToken)
    {
        var _httpClient = _httpClientFactory.CreateClient();
        var requestUrl = $"https://www.googleapis.com/oauth2/v3/userinfo?access_token={accessToken}";
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
            return null;

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GoogleUserInfo>(responseBody);
    }


}