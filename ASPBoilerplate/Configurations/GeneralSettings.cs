using System;

namespace ASPBoilerplate.Configurations;

public class GeneralSettings
{
    public static string DeveloperKey;
    public static void Initialize(WebApplicationBuilder builder) {
        DeveloperKey = builder.Configuration["GeneralSettings:DeveloperKey"];
    }
}
