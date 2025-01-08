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

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddControllers(options =>
{
    // options.Filters.Add<AuthenticationFilter>();
});

// Initialize Authentcation
AuthSettings.Initialize(builder);

// Initialize Mail settings
MailSettings.InitalizeMailSettings(builder);
MailService.ConfigureServices(builder.Services);

// Initialize Jwt config
JwtTokenSettings.Initialize(builder);

builder.Services.AddSqlite<AppDbContext>(connectionString);

var app = builder.Build();

// app.UseAntiforgery();

app.MapGet("/", () => "Connection is OK!");
// app.UseHttpsRedirection();
app.MapControllers();

app.FileRoutes();

app.Run();