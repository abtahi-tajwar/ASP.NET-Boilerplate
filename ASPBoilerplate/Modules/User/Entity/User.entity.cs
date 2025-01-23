using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Modules.User.Entity;

public class UserTokenEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [ForeignKey("UserId")]
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public string? DeviceSignature { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class UserOtpEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    // public RestrictedUserEntity UserEntity { get; set; } = null!;
    public required string Otp { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? ExpireAt { get; set; }
};