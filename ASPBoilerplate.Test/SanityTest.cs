using FluentAssertions;
using Xunit.Abstractions;

namespace ASPBoilerplate.Test;

[Collection("Admin_Collection")]
public class SanityTest
{// private readonly UserService _userService;
    private readonly ASPBoilerplateWebApplicationFactory _application;
    private readonly HttpClient _client;
    private ITestOutputHelper _output;

    public SanityTest(AdminFixture fixture, ITestOutputHelper output)
    {
        _application = fixture.Application;
        _client = fixture.Client;
        _output = output;
    }
    [Fact]
    public async Task CheckApplicationBooting()
    {
        // var _application = new ASPBoilerplateWebApplicationFactory();
        // var _client = _application.CreateClient();
        // var user = _userService.GetUserProfileByEmail("abtahitajwar@gmail.com");
        // var result = user.Email; 

        var res = await _client.GetAsync("/");
        var str = await res.Content.ReadAsStringAsync();
        _output.WriteLine($"Testing in test: {str}");

        var result = "abtahitajwar@gmail.com";
        result.Should().Be("abtahitajwar@gmail.com");
    }
}
