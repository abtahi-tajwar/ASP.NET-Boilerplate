using System;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Mvc;
using ASPBoilerplate.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    
}
