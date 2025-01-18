using ASPBoilerplate;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Utils;
using Hangfire;
using SignalRChat.Hubs;


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

// Logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Initialize General Settings
GeneralSettings.Initialize(builder);

// Initialize Authentcation
AuthSettings.Initialize(builder);

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


builder.Services.AddSignalR();

builder.Logging.AddConsole();

var app = builder.Build();

// Use the configured CORS policy
app.UseCors("AllowSpecificOrigins");

// app.UseAntiforgery();

app.MapGet("/", () => {
    HangfireSettings.InitializeJobs();
    return "Connection is OK!";
});
// app.UseHttpsRedirection();
app.MapControllers();
app.UseHangfireDashboard();
app.FileRoutes();
app.MapHub<ChatHub>("/chatHub");



app.Run();

