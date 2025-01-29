namespace ASPBoilerplate.Configurations;

public class GoogleAuthSettings {
    public static string? ClientId;
    public static string? ClientSecret;
    public static string RedirectEndpoint = "/signin-google";

    public static void Initialize(WebApplicationBuilder builder) {
        ClientId = builder.Configuration["Google:ClientId"];
        ClientSecret = builder.Configuration["Google:ClientSecret"];
    }
}