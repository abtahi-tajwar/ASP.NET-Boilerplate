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
    string Password,
    string Otp
);

public record LoginAdminDto (
    [Required]
    string Email,
    [Required]
    string Password,
    string? Device
);
public record LoginAdminResponseDto(
    RestrictedUserEntity User,
    string Token
);
