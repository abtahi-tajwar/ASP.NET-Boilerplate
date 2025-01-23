using System;
using System.Reflection;
using ASPBoilerplate.Modules.Auth;
using ASPBoilerplate.Services;

namespace ASPBoilerplate.Configurations;

public class GeneralSettings
{
    public static string DeveloperKey;
    public static void Initialize(WebApplicationBuilder builder)
    {
        DeveloperKey = builder.Configuration["GeneralSettings:DeveloperKey"];

        // Add Base Service
        builder.Services.AddScoped<CacheService>();
        builder.Services.AddScoped<AppBaseService>();

        // builder.Services.AddScoped<AuthService>();

        // Scan for services with ScopedServiceAttribute
        var assembly = Assembly.GetExecutingAssembly(); // Replace with specific assembly if needed
        var typesWithAttribute = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ScopedServiceAttribute>() != null && !t.IsAbstract && !t.IsInterface);

        foreach (var type in typesWithAttribute)
        {
            builder.Services.AddScoped(type);
        }


    }
}
