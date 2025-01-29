using ASPBoilerplate;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Utils;
using Hangfire;
using SignalRChat.Hubs;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ASPBoilerplate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddSqlite<AppDbContext>(connectionString);

        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy.WithOrigins("http://127.0.0.1:5500") // Specify allowed origins
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .SetIsOriginAllowedToAllowWildcardSubdomains();
            });
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }


        // Logger
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        // Swagger
        SwaggerSettings.Initialize(builder);


        // Initialize General Settings
        GeneralSettings.Initialize(builder);

        // Initialize Authentcation
        AuthSettings.Initialize(builder);
        GoogleAuthSettings.Initialize(builder);

        // Initialize Mail settings
        MailSettings.InitalizeMailSettings(builder);
        MailService.ConfigureServices(builder.Services);

        // Initialize Jwt config
        JwtTokenSettings.Initialize(builder);

        // Initialize Payment gateways
        StripeSettings.Initialize(builder);
        SSLCommerzeSettings.Initialize(builder);

        // Initialize scheduler
        HangfireSettings.Initialize(builder);

        // Initialize caching
        CacheSettings.Initialize(builder);

        // Initialize GraphQL
        GraphQLSettings.Initialize(builder);


        builder.Services.AddSignalR();

        builder.Logging.AddConsole();

        builder.Services.AddOpenApi();

        var app = builder.Build();


        app.MapOpenApi();

        // Use the configured CORS policy
        app.UseCors("AllowSpecificOrigins");

        // app.UseAntiforgery();

        app.MapGet("/", () =>
        {
            // HangfireSettings.InitializeJobs();
            return "Connection is OK!";
        });
        // app.UseHttpsRedirection();
        app.MapControllers();
        app.UseHangfireDashboard();
        app.MapHub<ChatHub>("/chatHub");
        app.MapGraphQL();

        // Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        app.Run();
    }
}



