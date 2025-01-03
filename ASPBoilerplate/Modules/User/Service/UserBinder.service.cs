using System;
using ASPBoilerplate.Modules.User.Dtos;
using ASPBoilerplate.Modules.User.Entity;
using ASPBoilerplate.Utils;

namespace ASPBoilerplate.Modules.User.Service;

public class UserBinder
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
}
