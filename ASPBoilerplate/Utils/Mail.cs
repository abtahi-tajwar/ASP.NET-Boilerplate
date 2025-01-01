using System;

namespace ASPBoilerplate.Utils;
using Mailtrap.Source.Models;

public class Mail
{
    private readonly MailtrapSender _mailtrap;
    public Mail()
    {
        _mailtrap = new MailtrapSender("smtp@mailtrap.io", "ce8e91d0df0043dc8e662df1a27d294b");
    }

    public void SetOTP(string otp, string email)
    {
        var newEmail = new Email(
            email,
            "smtp@mailtrap.io",
            "OTP for your account",
            $"OTP {otp}");

        _mailtrap.Send(newEmail);

    }
}
