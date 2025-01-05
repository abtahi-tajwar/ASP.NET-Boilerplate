using System;
using ASPBoilerplate.Modules.User.Entity;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Modules.User;

public class BaseUserSeeder
{
    public static RestrictedUserEntity GetSuperAdmin()
    {
        return new RestrictedUserEntity()
        {
            Username = "Super Admin 1",
            Role = USER_ROLES.SUPER_ADMIN,
            Email = "abtahitajwar@gmail.com"
        };
    }
    public static void Seed(DbContext context)
    {
        var superAdmin = GetSuperAdmin();
        context.Add(superAdmin);
        context.SaveChanges();
    }
}
