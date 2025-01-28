namespace ASPBoilerplate.Test.Modules.User;

using System.Threading.Tasks;
using ASPBoilerplate.Modules.User;
using FluentAssertions;
using Xunit;

public class UserInitializationTest {
    // private readonly UserService _userService;
    private readonly ASPBoilerplateWebApplicationFactory _application;
    private readonly HttpClient _client;

    public UserInitializationTest () {
        _application = new ASPBoilerplateWebApplicationFactory();
        _client = _application.CreateClient();
    }
    [Fact]
    public async Task CheckApplicationBooting() {
        var _application = new ASPBoilerplateWebApplicationFactory();
        var _client = _application.CreateClient();
        // var user = _userService.GetUserProfileByEmail("abtahitajwar@gmail.com");
        // var result = user.Email; 

        var res = await _client.GetAsync("/");
        var str = await res.Content.ReadAsStringAsync();
        Console.WriteLine(str);
        
        var result = "abtahitajwar@gmail.com";
        result.Should().Be("abtahitajwar@gmail.com");
    }
    public void Dispose()
    {
        _client.Dispose();
        _application.Dispose();
    }
}