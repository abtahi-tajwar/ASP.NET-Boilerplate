using System;
using ASPBoilerplate.Filters;
using ASPBoilerplate.Modules.Auth;
using ASPBoilerplate.Shared.JwtToken;
using Microsoft.AspNetCore.Mvc;

namespace ASPBoilerplate.Controllers;

/**
    The user sign up process will be 2 different kinds
    1. Resitrcted access user sign up
        a. User will be created by the admin
        b. User will receieve OTP to their email to verify - Done
        c. User will be able to set their password - Done
        d. User will be able to login with their email and password - 
        e. Then they will have to update their profile
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
    private readonly AuthService _service;
    public AuthControllerAdmin(AuthService service)
    {
        _service = service;
    }
    [HttpPost("get-otp", Name = "GetOtpAdmin")]
    public IResult GetOtpAdmin(GetOtpAdminDto body, AppDbContext context)
    {
        try
        {
            var Otp = _service.GetOtp(body.Email);
            return CustomResponse.Ok("Otp sent to users inbox, please check");
        }
        catch (Exception e)
        {
            return CustomResponse.BadRequest(e.Message);
        }
    }

    [HttpPost("set-password", Name = "SetPasswordAdmin")]
    public IResult SetPasswordAdmin (SetPasswordAdminDto body, AppDbContext context) {
        try {
            _service.SetPasswordAdmin(body.Email, body.Password, body.Otp);
            return CustomResponse.Ok(null, "Password Successfully Set");
        } catch (Exception e) {
            return CustomResponse.BadRequest($"{e.Message}");
        }     
    }

    [HttpPost("login", Name="LoginAdmin")]
    public IResult LoginAdmin (LoginAdminDto body, AppDbContext context, ILogger<AuthControllerAdmin> logger) {
        try {
            logger.LogInformation("Starting user login process...");
            var response = _service.Login(body.Email, body.Password, body.Device);
            
            return CustomResponse.Ok(response, "Logged In Successfully");
        } catch (Exception e) {
            return CustomResponse.BadRequest(e.Message);
        }
    }
}

[ApiController]
[Route("/dev/auth")]
public class AuthControllerAdminDev : ControllerBase {
    
    private readonly AuthService _service;
    public AuthControllerAdminDev (AuthService service) {
        _service = service;
    }

    [HttpPost("set-password")]
    [DevAuthorization]
    public IResult SetPasswordAdminDev (SetPasswordAdminDto body, AppDbContext context) {
        try {
            _service.SetPasswordAdminDev(body.Email, body.Password, body.Otp);
            return CustomResponse.Ok(null, "Password Successfully Set");
        } catch (Exception e) {
            return CustomResponse.BadRequest($"{e.Message}");
        }  
    }
}



