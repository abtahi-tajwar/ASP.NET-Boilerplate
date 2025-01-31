namespace ASPBoilerplate.Services;

public class FacebookAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public FacebookAuthService(IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClient;
        _httpContextAccessor = httpContextAccessor;


    }

    public async Task<string> GetFacebookProfileAsync(string accessToken)
    {
        using var httpClient = new HttpClient();
        var facebookGraphUrl = $"https://graph.facebook.com/v22.0/me?fields=id,name,email,picture&access_token={accessToken}";

        var response = await httpClient.GetAsync(facebookGraphUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }

        return "Failed to retrieve profile information.";
    }


}