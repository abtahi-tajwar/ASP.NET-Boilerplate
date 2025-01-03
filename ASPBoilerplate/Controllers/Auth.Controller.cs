using System;
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
    public IResult SignUp () {
        return CustomResponse.Ok("User signed up");
    }
}

[Route("/auth/admin")]
public class AuthControllerAdmin : ControllerBase
{
    [HttpPost("signup", Name = "AdminSignUp")]
    public IResult CreateUserAdmin () {
        return CustomResponse.Ok("User email registered");
    }
}
