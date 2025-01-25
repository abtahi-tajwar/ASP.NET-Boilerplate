using ASPBoilerplate.Modules.User;
using ASPBoilerplate.Modules.User.Entity;

namespace ASPBoilerplate.Services;

class Query {
    public RestrictedUserEntity GetUser() =>
        new RestrictedUserEntity
        {
            Username = "User er nam",
            Email = "useremail@gmail.com",
            Password = "userpassword",
            Role = USER_ROLES.ADMIN
        };
}