using ASPBoilerplate.Modules.File;
using ASPBoilerplate.Utils;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Connection is OK!");
app.FileRoutes();

app.Run();

struct _FilePost
{
    public string Name;
    public string Storage;
};