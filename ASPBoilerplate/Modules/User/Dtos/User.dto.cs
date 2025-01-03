using System;
using System.ComponentModel.DataAnnotations;

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
