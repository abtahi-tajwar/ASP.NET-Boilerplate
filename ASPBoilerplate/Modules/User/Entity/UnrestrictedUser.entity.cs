using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBoilerplate.Modules.User;

public class UnrestrictedUserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string  Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public List<UnrestrictedTokenEntity>? Tokens { get; set; }
    public UnrestrictedUserEntity? Profile { get; set; }
    public UnrestrictedUserUserOtpEntity? Otp { get; set; }
};

public class UnrestrictedTokenEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public UnrestrictedUserEntity UserEntity { get; set; } = null!;
    public required string Otp { get; set; }
    public string? DeviceSignature { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class UnrestrictedUserUserOtpEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string Otp { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? ExpireAt { get; set; }
};
public class UnrestrictedUserProfileEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public UnrestrictedUserEntity UserEntity { get; set; } = null!;
    public required string Name { get; set; }
    public DateOnly? DOB { get; set; }
}