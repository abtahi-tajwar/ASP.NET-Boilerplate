using System;
using System.ComponentModel.DataAnnotations;
using ASPBoilerplate.Modules.User.Entity;

namespace ASPBoilerplate.Modules.User.Dtos;

public record CreateUserAdminDto (
    [Required]
    [MaxLength(20)]
    string UserName,
    [Required]
    [EmailAddress]
    string Email,
    [Required]
    [EnumValidatorFilter(typeof(USER_ROLES), ErrorMessage = "Cannot detect User type")]
    string Role
);

public record GetRestrictedUserDetailsResponseDto (
    string Id,
    string UserName,
    string?  Email,
    bool IsEmailConfirmed,
    bool IsPasswordSet,
    USER_ROLES Role,
    RestrictedUserProfileEntity? Profile
);
