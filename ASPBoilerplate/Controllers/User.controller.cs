using System;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Mvc;
using ASPBoilerplate.Filters;
using ASPBoilerplate.Modules.Auth;

namespace ASPBoilerplate.Controllers;

[ApiController]
[Route("/user")]public class User : ControllerBase
{
    [HttpGet("list", Name = "ListUsers")]
    [AuthorizationFilter("Admin")]
    public IEnumerable<UserEntity> List(AppDbContext context)
    {
        var users = context.Users.ToList();
        return users;
    }

    [HttpPost("verify-email", Name = "VerifyEmail")]
    public IResult CreateUser(VerifyEmailDto body, AppDbContext context)
    {
        var service = new UserService(context);

        service.GetOtp(body.Email);
        return CustomResponse.Ok("OTP sent to email");
    }

    

    /**
    The user sign up process will be 2 different kinds
    1. Admin panel user sign up
        a. User will be created by the admin
        b. User will be sent an email with a link to set their password
        c. User will be able to login with their email and password
        d. Then they will have to update their profile
    2. User facing signup
        a. User will sign up with their email and password
        b. User will be sent an email with a link to set their password
        c. User will be able to login with their email and password
        d. Then they will have to update their profile
    */
    
}
