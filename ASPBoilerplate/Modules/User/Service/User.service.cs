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

    public GetRestrictedUserDetailsResponseDto GetUserProfile (string UserId) {
        var FoundUser = _context.RestrictedUsers.FirstOrDefault(user => user.Id == UserId);
        if (FoundUser == null) {
            throw new Exception("Can't extract profile information of this user");
        }
        var User = RestrictedUserBinder.GetUserDetailsEntityToDto(FoundUser);
        return User;
    }
    public GetRestrictedUserDetailsResponseDto GetUserProfileByEmail (string email) {
        var FoundUser = _context.RestrictedUsers.FirstOrDefault(user => user.Email == email);
        if (FoundUser == null) {
            throw new Exception("Can't extract profile information of this user");
        }
        var User = RestrictedUserBinder.GetUserDetailsEntityToDto(FoundUser);
        return User;
    }

}
