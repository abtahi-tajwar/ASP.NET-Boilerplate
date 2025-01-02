using ASPBoilerplate;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Filters;
using ASPBoilerplate.Middlewares;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Utils;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddControllers(options =>
{
    // options.Filters.Add<AuthenticationFilter>();
});

MailSettings.InitalizeMailSettings(builder);
MailService.ConfigureServices(builder.Services);

builder.Services.AddSqlite<AppDbContext>(connectionString);

var app = builder.Build();

// app.UseAntiforgery();


app.MapGet("/", () => "Connection is OK!"); 
// app.UseHttpsRedirection();
app.MapControllers();

app.FileRoutes();

app.Run();