using System;
using ASPBoilerplate.Services;
using Hangfire;
using Hangfire.SQLite;

namespace ASPBoilerplate.Configurations;

public class HangfireSettings
{
    public static string? DatabaseConnection;

    public static void Initialize(WebApplicationBuilder builder)
    {
        DatabaseConnection = builder.Configuration["ConnectionStrings:HangfireDatabaseConnection"];

        // Add Hangfire services.
        builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage(DatabaseConnection));

        // Add the processing server as IHostedService
        builder.Services.AddHangfireServer();

        // Add framework services.
        // builder.Services.AddMvc();
    }

    public static void InitializeJobs()
    {
        Console.WriteLine("Cron Job started");
        RecurringJob.AddOrUpdate(
            "TestJob",
            () => CronJobService.TestJob(),
            "*/5 * * * * *",
            new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Utc,  // Use UTC for scheduling
            }
        );
    }
}
