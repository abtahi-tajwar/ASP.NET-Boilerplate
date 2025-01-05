using System.ComponentModel.DataAnnotations;

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