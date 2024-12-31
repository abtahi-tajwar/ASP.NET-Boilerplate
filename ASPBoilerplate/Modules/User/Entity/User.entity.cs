using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.SignalR;

namespace ASPBoilerplate.Modules.User;

public class UserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public required string  Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public required USER_ROLES Role { get; set; }
    public List<UserTokenEntity>? Tokens { get; set; }
    public UserProfileEntity? Profile { get; set;}

};


public class UserTokenEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public UserEntity UserEntity { get; set; } = null!;
    public required string value { get; set; }
    public string? DeviceSignature { get; set; }
    public DateTime? ValidUnit { get; set; }
    public required DateTime CreatedAt { get; set; }
};

public class UserProfileEntity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public UserEntity UserEntity { get; set; } = null!;
    public required string Name { get; set; }
    public DateOnly? DOB { get; set; }
}