using System;
using Stripe;

namespace ASPBoilerplate.Configurations;

public class StripeSettings
{
    public static string? SecretKey { get; set; }

    public static void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
        SecretKey = builder.Configuration["Stripe:SecretKey"];
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
    }
}
