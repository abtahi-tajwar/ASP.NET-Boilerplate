using System;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Modules.User.Entity;
using ASPBoilerplate.Shared.JwtToken;
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

    public void SetPasswordAdmin(string email, string password, string otp)
    {
        RestrictedUserEntity? User = _context.RestrictedUsers
            .Include(u => u.Otp)
            .FirstOrDefault(User => User.Email == email);
        if (User == null)
        {
            throw new Exception("Not user registered using this email");
        }

        if (User.Otp?.Otp != otp) {
            throw new Exception("Otp does not match, cannot set password");
        }

        var HashedPassword = PasswordHasher.HashPassword(password);

        User.Password = HashedPassword;
        User.IsPasswordSet = true;

        _context.SaveChanges();
    }

    public LoginAdminResponseDto Login(string Email, string Password, string? Device = "")
    {
        var User = _context.RestrictedUsers.FirstOrDefault(u => u.Email == Email);
        if (User == null)
        {
            throw new Exception("Wrong email/password provided");
        }
        if (User.Password == null)
        {
            throw new Exception("Please set password first");
        }
        if (!PasswordHasher.AreHashesEqual(Password, User.Password!))
        {
            throw new Exception("Wrong email/password provided");
        }

        var token = JwtTokenService.GenerateToken(new JwtTokenPayload()
        {
            UserId = User.Id,
            Email = Email,
            Role = User.Role.ToString()
        });

        var ExistingToken = _context.RestrictedUserTokens.FirstOrDefault(token => (token.UserId == User.Id && token.DeviceSignature == Device));

        if (ExistingToken == null)
        {

            _context.RestrictedUserTokens.Add(new()
            {
                Token = token,
                UserId = User.Id,
                DeviceSignature = Device,
                Expiration = DateTime.UtcNow.AddHours(JwtTokenSettings.ExpireInHour),
                CreatedAt = DateTime.UtcNow
            });
        } else {
            ExistingToken.Token = token;
        }

        _context.SaveChanges();

        var response = new LoginAdminResponseDto(
            User: User,
            Token: token
        );

        return response;
    }


    // Dev methods
    public void SetPasswordAdminDev(string email, string password, string otp)
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

}
