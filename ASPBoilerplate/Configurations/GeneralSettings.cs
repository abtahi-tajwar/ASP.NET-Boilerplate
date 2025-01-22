using System;
using ASPBoilerplate.Services;

namespace ASPBoilerplate.Configurations;

public class GeneralSettings
{
    public static string DeveloperKey;
    public static void Initialize(WebApplicationBuilder builder) {
        DeveloperKey = builder.Configuration["GeneralSettings:DeveloperKey"];

        // Add Base Service
        builder.Services.AddScoped<CacheService>();
        builder.Services.AddScoped<AppBaseService>();
        
    }
}
