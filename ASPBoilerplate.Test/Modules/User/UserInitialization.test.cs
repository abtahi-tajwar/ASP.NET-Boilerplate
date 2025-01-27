namespace ASPBoilerplate.Test.Modules.User;

using ASPBoilerplate.Modules.User;
using FluentAssertions;
using Xunit;

public class UserInitializationTest {
    private readonly UserService _userService;

    public UserInitializationTest (UserService userService) {
        _userService = userService;
    }
    [Fact]
    public void Super_Admin_Seeded() {
        var user = _userService.GetUserProfileByEmail("abtahitajwar@gmail.com");
        var result = user.Email;
        result.Should().Be("abtahitajwar@gmail.com");
    }
}