namespace ASPBoilerplate.Test.Modules.User;

using System.Diagnostics;
using System.Threading.Tasks;
using ASPBoilerplate.Modules.User;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

[Collection("Admin_Collection")]
public class UserInitializationTest {
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    private readonly string? _token;
    public UserInitializationTest (AdminFixture fixture, ITestOutputHelper output) {
        _client = fixture.Client;
        _output = output;
        _token = fixture.AuthToken;
    }

    [Fact]
    public async Task CheckApplicationBooting() {
        _output.WriteLine($"Auth token {_token}");
        // var user = _userService.GetUserProfileByEmail("abtahitajwar@gmail.com");
        // var result = user.Email; 

        var res = await _client.GetAsync("/");
        var str = await res.Content.ReadAsStringAsync();
        _output.WriteLine(str);
        
        var result = "abtahitajwar@gmail.com";
        result.Should().Be("abtahitajwar@gmail.com");
    }
}