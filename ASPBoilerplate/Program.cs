using System.Text;
using ASPBoilerplate;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Filters;
using ASPBoilerplate.Middlewares;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using SignalRChat.Hubs;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
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

// Initialize General Settings
GeneralSettings.Initialize(builder);

// Initialize Authentcation
AuthSettings.Initialize(builder);

// Initialize Mail settings
MailSettings.InitalizeMailSettings(builder);
MailService.ConfigureServices(builder.Services);

// Initialize Jwt config
JwtTokenSettings.Initialize(builder);

builder.Services.AddSqlite<AppDbContext>(connectionString);
builder.Services.AddSignalR();

var app = builder.Build();

// Use the configured CORS policy
app.UseCors("AllowSpecificOrigins");

// app.UseAntiforgery();

app.MapGet("/", () => "Connection is OK!");
// app.UseHttpsRedirection();
app.MapControllers();

app.FileRoutes();
app.MapHub<ChatHub>("/chatHub");


app.Run();