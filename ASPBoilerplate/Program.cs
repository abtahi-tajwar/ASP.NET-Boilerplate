using ASPBoilerplate;
using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Utils;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddSqlite<AppDbContext>(connectionString);

var app = builder.Build();
// app.UseAntiforgery();

app.MapGet("/", () => "Connection is OK!");
app.FileRoutes();

app.Run();