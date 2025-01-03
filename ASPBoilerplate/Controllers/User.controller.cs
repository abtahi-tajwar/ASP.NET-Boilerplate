using System;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Mvc;
using ASPBoilerplate.Filters;
using ASPBoilerplate.Modules.Auth;
using ASPBoilerplate.Modules.User.Dtos;
using ASPBoilerplate.Modules.User.Entity;

namespace ASPBoilerplate.Controllers;

[ApiController]
[Route("/user")]public class UserController : ControllerBase
{
    [HttpGet("list", Name = "ListUsers")]
    [AuthorizationFilter("Admin")]
    public IEnumerable<UnrestrictedUserEntity> List(AppDbContext context)
    {
        var users = context.UnrestrictedUsers.ToList();
        return users;
    }

    [HttpPost("verify-email", Name = "VerifyEmail")]
    public IResult CreateUser(VerifyEmailDto body, AppDbContext context)
    {
        var service = new UserService(context);

        service.GetOtp(body.Email);
        return CustomResponse.Ok("OTP sent to email");
    }
    
}

[ApiController]
[Route("admin/user")]
public class UserControllerAdmin : ControllerBase{
    [HttpGet("list", Name = "ListUsersAdmin")]
    public IEnumerable<RestrictedUserEntity> List(AppDbContext context)
    {
        var users = context.RestrictedUsers.ToList();
        return users;
    }
    [HttpPost("create", Name = "CreateUserAdmin")]
    public IResult CreateUserAdmin (CreateUserAdminDto body, AppDbContext context) {
        var service = new UserService(context);
        RestrictedUserEntity newUser = service.RegisterUserEmailAdmin(body);
        return CustomResponse.Ok(newUser, "User Registered");
    }   
    
}
