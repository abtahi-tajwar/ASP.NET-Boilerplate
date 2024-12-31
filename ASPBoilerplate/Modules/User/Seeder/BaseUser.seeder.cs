using System;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Modules.User;

public class BaseUserSeeder
{
    public static UserEntity GetSuperAdmin()
    {
        return new UserEntity()
        {
            Username = "Super Admin 1",
            Role = USER_ROLES.SUPER_ADMIN
        };
    }
    public static void Seed(DbContext context)
    {
        var superAdmin = GetSuperAdmin();
        context.Add(superAdmin);
        context.SaveChanges();
    }
}
