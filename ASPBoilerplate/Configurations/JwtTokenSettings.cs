using System;

namespace ASPBoilerplate.Configurations;

public class JwtTokenSettings
{
    public static string? Issuer { get; set; }
    public static string? Audience { get; set; }
    public static string? Secret { get; set; }
    public static int ExpireInHour { get; set;}
    public static void Initialize(WebApplicationBuilder builder) {
        Issuer = builder.Configuration["JwtToken:Issuer"];
        Audience = builder.Configuration["JwtToken:Audience"];
        Secret = builder.Configuration["JwtToken:Secret"];
        ExpireInHour = 48;
    }
}
