using System;
using ASPBoilerplate.Modules.User.Entity;
using ASPBoilerplate.Utils;
using Microsoft.EntityFrameworkCore;

namespace ASPBoilerplate.Modules.Auth;

public class AuthService
{
    public AppDbContext _context;
    public AuthService(AppDbContext context)
    {
        _context = context;
    }
    public RestrictedUserUserOtpEntity GetOtp(string email)
    {
        RestrictedUserEntity User = _context.RestrictedUsers.FirstOrDefault(User => User.Email == email);
        if (User == null)
        {
            throw new Exception("Not user registered using this email");
        }
        var mail = new MailService();
        int Otp = mail.SetOTP(email);
        RestrictedUserUserOtpEntity OtpEntry = new()
        {
            UserId = User.Id,
            Otp = Otp.ToString(),
            CreatedAt = DateTime.Now,
            ExpireAt = DateTime.Today.AddHours(48)
        };
        _context.RestrictedUserOtps.Add(OtpEntry);
        User.Otp = OtpEntry;

        _context.SaveChanges();

        return OtpEntry;
    }

    public Boolean VerifyOtp(string email, string otp)
    {
        RestrictedUserEntity? User = _context.RestrictedUsers
            .Include(u => u.Otp)
            .FirstOrDefault(User => User.Email == email);

        if (User == null)
        {
            throw new Exception("Not user registered using this email");
        }
        if (User.Otp == null)
        {
            return false;
        }
        if (otp == User.Otp.Otp && DateTime.Now < User.Otp.ExpireAt)
        {
            User.Otp = null;
            User.IsEmailConfirmed = true;
            _context.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPasswordAdmin(string email, string password)
    {
        RestrictedUserEntity? User = _context.RestrictedUsers
            .Include(u => u.Otp)
            .FirstOrDefault(User => User.Email == email);

        if (User == null)
        {
            throw new Exception("Not user registered using this email");
        }

        var HashedPassword = PasswordHasher.HashPassword(password);

        User.Password = HashedPassword;
        User.IsPasswordSet = true;

        _context.SaveChanges();
    }

    public LoginAdminResponseDto Login(string Email, string Password)
    {
        var User = _context.RestrictedUsers.FirstOrDefault(u => u.Email == Email);
        if (User == null)
        {
            throw new Exception("Wrong email/password provided");
        }
        var response = new LoginAdminResponseDto(
            User: User,
            Token: ""
        );
        return response;
    }

}
