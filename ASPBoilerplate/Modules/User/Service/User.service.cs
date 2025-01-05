using System;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Modules.User.Dtos;
using ASPBoilerplate.Modules.User.Entity;
using ASPBoilerplate.Modules.User.Service;
using ASPBoilerplate.Utils;
using Microsoft.AspNetCore.Identity;

namespace ASPBoilerplate.Modules.User;

public class UserService
{
    AppDbContext _context;
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public void GetOtp(string email)
    {
        var mail = new MailService();
        int otp = mail.SetOTP(email);
    }

    public RestrictedUserEntity RegisterUserEmailAdmin(CreateUserAdminDto user)
    {
        RestrictedUserEntity newUser = RestrictedUserBinder.CreateUserAdminDtoToEntity(user);
        _context.RestrictedUsers.Add(newUser);
        _context.SaveChanges();

        return newUser;
    }
}
