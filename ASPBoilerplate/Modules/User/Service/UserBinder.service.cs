using System;
using ASPBoilerplate.Modules.User.Dtos;
using ASPBoilerplate.Modules.User.Entity;
using ASPBoilerplate.Utils;

namespace ASPBoilerplate.Modules.User.Service;

public class RestrictedUserBinder
{
    public static RestrictedUserEntity CreateUserAdminDtoToEntity (CreateUserAdminDto user) {
        USER_ROLES Role = Helpers.ParseEnum<USER_ROLES>(user.Role);
        RestrictedUserEntity newUser = new() {
            Username = user.UserName,
            Email = user.Email,
            Role = Role
        };
        return newUser;
    }
    
    public static GetRestrictedUserDetailsResponseDto GetUserDetailsEntityToDto (RestrictedUserEntity User) {
        GetRestrictedUserDetailsResponseDto Result = new (
            Id: User.Id,
            UserName: User.Username,
            Email: User.Email,
            IsEmailConfirmed: User.IsEmailConfirmed,
            IsPasswordSet: User.IsPasswordSet,
            Role: User.Role,
            Profile: User.Profile
        );
        return Result;
    }
}
