using ASPBoilerplate.Controllers;
using ASPBoilerplate.Modules.Auth;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace ASPBoilerplate.Test;

public class AdminFixture : IDisposable
{
    // private readonly UserService _userService;
    public ASPBoilerplateWebApplicationFactory Application { get; }
    public HttpClient Client { get; }
    private string _databaseFilePath;

    private string _testUserEmail = "testuser@gmail.com";
    private string _testUserPassword = "Test123!";
    public string? AuthToken;

    public AdminFixture()
    {
        Application = new ASPBoilerplateWebApplicationFactory();
        Client = Application.CreateClient();

        SeedDatabase();
        InitializeUser();

    }
    private void SeedDatabase()
    {
        var scope = Application.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();

        var dbConnection = dbContext.Database.GetDbConnection();

        if (dbConnection is Microsoft.Data.Sqlite.SqliteConnection sqliteConnection)
        {
            _databaseFilePath = sqliteConnection.DataSource; // Path to the database file
        }

        dbContext.RestrictedUsers.Add(new()
        {
            Username = "Super Admin 1",
            Role = USER_ROLES.SUPER_ADMIN,
            Email = "testuser@gmail.com"
        });
        // dbContext.RestrictedUsers.Add(new()
        // {
        //     Username = "Super Test 1",
        //     Role = USER_ROLES.ADMIN,
        //     Email = "samaheer.zameel@gmail.com"
        // });

        dbContext.SaveChanges();
    }

    private void InitializeUser () {
        var scope = Application.Services.CreateScope();
        // var authControllerAdminDev = scope.ServiceProvider.GetRequiredService<AuthControllerAdminDev>();
        var authService = scope.ServiceProvider.GetRequiredService<AuthService>();
        // var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var body = new SetPasswordAdminDto(
            Email: _testUserEmail,
            Password: _testUserPassword,
            Otp: "00000"
        );
        authService.SetPasswordAdminDev(body.Email, body.Password, "00000");
        var response = authService.Login(body.Email, body.Password, "IntegrationTest");
        AuthToken = response.Token;
    }
    public void Dispose()
    {
        Client.Dispose();
        Application.Dispose();

        // Delete the database file
        if (!string.IsNullOrEmpty(_databaseFilePath) && File.Exists(_databaseFilePath))
        {
            File.Delete(_databaseFilePath);
            Console.WriteLine($"Deleted database file: {_databaseFilePath}");
        }

    }
}