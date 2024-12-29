using ASPBoilerplate;
using ASPBoilerplate.Middlewares;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Utils;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddControllers();
builder.Services.AddSqlite<AppDbContext>(connectionString);

var app = builder.Build();

// app.UseAntiforgery();


app.MapGet("/", () => "Connection is OK!");
app.UseHttpsRedirection();
app.MapControllers();

app.FileRoutes();

app.Run();