using System;
using ASPBoilerplate.Modules.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ASPBoilerplate.Controllers;

/**
    The user sign up process will be 2 different kinds
    1. Resitrcted access user sign up
        a. User will be created by the admin
        b. User will be sent an email with a link to set their password
        c. User will be able to login with their email and password
        d. Then they will have to update their profile
    2. General user signup
        a. User will sign up with their email and password
        b. User will be sent an email with a link to set their password
        c. User will be able to login with their email and password
        d. Then they will have to update their profile
    */

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("signup", Name = "SignUp")]
    public IResult SignUp()
    {
        return CustomResponse.Ok("User signed up");
    }
}

[ApiController]
[Route("/admin/auth")]
public class AuthControllerAdmin : ControllerBase
{
    [HttpPost("get-otp", Name = "GetOtpAdmin")]
    public IResult GetOtpAdmin(GetOtpAdminDto body, AppDbContext context)
    {
        try
        {
            var service = new AuthService(context);
            var Otp = service.GetOtp(body.Email);
            return CustomResponse.Ok("Otp sent to users inbox, please check");
        }
        catch (Exception e)
        {
            return CustomResponse.BadRequest(e.Message);
        }
    }

    [HttpPost("verify-otp", Name = "VerifyOtpAdmin")]
    public IResult VerifyOtpAdmin (VerifyOtpAdminDto body, AppDbContext context) {
        try {
            var service = new AuthService(context);
            var otpValid = service.VerifyOtp(body.Email, body.Otp);
            
            if (otpValid) {
                return CustomResponse.Ok(null, "OTP Verified Successfully!");
            } else {
                return CustomResponse.BadRequest("OTP Does not match!");
            }
        } catch (Exception e) {
            return CustomResponse.BadRequest(e.Message);
        }
    }

    [HttpPost("set-password", Name = "SetPasswordAdmin")]
    public IResult SetPasswordAdmin (SetPasswordAdminDto body, AppDbContext context) {
        try {
            var service = new AuthService(context);
            service.SetPasswordAdmin(body.Email, body.Password);
            return CustomResponse.Ok(null, "Password Successfully Set");
        } catch (Exception e) {
            return CustomResponse.BadRequest($"{e.Message}");
        }     
    }
}

