using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Modules.User.Entity;

[Index(nameof(Email), IsUnique = true)]
public class RestrictedUserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string  Username { get; set; }
    public string?  Email { get; set; }
    public string? Password { get; set; }
    public bool IsEmailConfirmed { get; set; } = false;
    public bool IsPasswordSet { get; set; } = false;
    public required USER_ROLES Role { get; set; }
    public List<UserTokenEntity>? Tokens { get; set; }
    public RestrictedUserProfileEntity? Profile { get; set; }
    public UserOtpEntity? Otp { get; set; }
}



public class RestrictedUserProfileEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public RestrictedUserEntity UserEntity { get; set; } = null!;
    public required string Name { get; set; }
    public DateOnly? DOB { get; set; }
}