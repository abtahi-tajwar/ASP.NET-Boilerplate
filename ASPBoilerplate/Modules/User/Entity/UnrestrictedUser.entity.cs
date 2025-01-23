using System;
using System.ComponentModel.DataAnnotations.Schema;
using ASPBoilerplate.Modules.User.Entity;

namespace ASPBoilerplate.Modules.User;

public class UnrestrictedUserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string  Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public List<UserTokenEntity>? Tokens { get; set; }
    public UnrestrictedUserEntity? Profile { get; set; }
    public UserOtpEntity? Otp { get; set; }
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