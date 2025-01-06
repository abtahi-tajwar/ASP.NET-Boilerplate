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
    public List<RestrictedUserTokenEntity>? Tokens { get; set; }
    public RestrictedUserEntity? Profile { get; set; }
    public RestrictedUserUserOtpEntity? Otp { get; set; }
}


public class RestrictedUserUserOtpEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    // public RestrictedUserEntity UserEntity { get; set; } = null!;
    public required string Otp { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? ExpireAt { get; set; }
};

public class RestrictedUserTokenEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [ForeignKey("UserId")]
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public string? DeviceSignature { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? CreatedAt { get; set; }
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