using System.ComponentModel.DataAnnotations;
using ASPBoilerplate.Modules.User.Entity;

namespace ASPBoilerplate.Modules.Auth;

public record GetOtpAdminDto (
    [Required]
    [EmailAddress]
    string Email
);
public record VerifyOtpAdminDto (
    [Required]
    string Otp,
    [Required]
    string Email
);

public record SetPasswordAdminDto (
    [Required]
    string Email,
    string Password
);

public record LoginAdminDto (
    [Required]
    string Email,
    [Required]
    string Password
);
public record LoginAdminResponseDto(
    RestrictedUserEntity User,
    string Token
);
