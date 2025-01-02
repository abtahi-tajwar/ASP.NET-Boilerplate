using System;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Utils;

namespace ASPBoilerplate.Modules.User;

public class UserService
{
    AppDbContext _context;
    public UserService(AppDbContext context) {
        _context = context;
    }

    public void GetOtp (string email) {
        var mail = new MailService();
        int otp = mail.SetOTP(email);
    }
}
