using System;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Modules.User.Entity;
using ASPBoilerplate.Services;
using ASPBoilerplate.Shared.JwtToken;
using ASPBoilerplate.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Stripe;

namespace ASPBoilerplate.Modules.Auth;

[ScopedService]
public class AuthService
{
    public AppDbContext _context;
    public CacheService _cache;

    public AuthService(AppDbContext context, CacheService cache)
    {
        _context = context;
        _cache = cache;
    }
    public UserOtpEntity GetOtp(string email)
    {
        RestrictedUserEntity? User = _context.RestrictedUsers.FirstOrDefault(User => User.Email == email);
        if (User == null)
        {
            throw new Exception("Not user registered using this email");
        }
        var mail = new MailService();
        int Otp = mail.SetOTP(email);
        UserOtpEntity OtpEntry = new()
        {
            UserId = User.Id,
            Otp = Otp.ToString(),
            CreatedAt = DateTime.Now,
            ExpireAt = DateTime.Today.AddHours(48)
        };
        _context.UserOtps.Add(OtpEntry);
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

        if (User.Otp?.Otp != otp)
        {
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
            Role = User.Role.ToString(),
            Device = Device
        });


        var NewToken = GenerateUserToken(User, Email, Device);

        var response = new LoginAdminResponseDto(
            User: User,
            Token: NewToken.Token
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

    public UserTokenEntity GenerateUserToken(RestrictedUserEntity user, string email, string? device)
    {

        var NewToken = JwtTokenService.GenerateToken(new JwtTokenPayload()
        {
            UserId = user.Id,
            Email = email,
            Role = user.Role.ToString(),
            Device = device
        });

        var ExistingToken = _context.UserTokens.FirstOrDefault(token => (token.UserId == user.Id && token.DeviceSignature == device));

        if (ExistingToken == null)
        {

            _context.UserTokens.Add(NewToken);
        }
        else
        {
            ExistingToken.Token = NewToken.Token;
        }

        string DeviceSnakeCase = Helpers.ConvertToSnakCase(device);

        _cache.Create($"user:{DeviceSnakeCase}", NewToken);

        _context.SaveChanges();

        return NewToken;
    }

    public TokenValidationResponse ValidateToken (string? token) {
        if (token == null) return null;
        try {
            var Res = JwtTokenService.DecodeAndValidateToken(token);
            // _cache.GetString($"Something {Res.UserId}_{Res.Email}_{Res.Role}");
            Console.WriteLine($"Something {Res.UserId}_{Res.Email}_{Res.Role}_{Res.Device}");

            var Token = _cache.Get<UserTokenEntity>($"user:{Helpers.ConvertToSnakCase(Res.Device)}");

            if (Token == null) {
                Token = _context.UserTokens.FirstOrDefault(t => t.Token == token);
            }

            if (Token.Expiration < DateTime.UtcNow) {
                throw new Exception("Token expired");
            }
            
            if (Token.Token != token) {
                throw new Exception("Invalid token!");
            }

            return new TokenValidationResponse(Token, Res.Role, true);
        } catch (Exception e) {
            Console.WriteLine($"Access unauthorized. {e.Message}");
            return new TokenValidationResponse(null, null, false);
        }
        
    }

}
