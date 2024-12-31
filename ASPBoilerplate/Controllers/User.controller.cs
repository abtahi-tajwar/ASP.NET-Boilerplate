using System;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Mvc;
using ASPBoilerplate.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ASPBoilerplate.Controllers;

[ApiController]
[Route("/user")]public class User : ControllerBase
{
    [HttpGet("list", Name = "ListUsers")]
    [AuthorizationFilter("Admin")]
    public IEnumerable<UserEntity> List()
    {
        var user = new UserEntity
        {
            Id = "222",
            Username = "Abtahi Tajwar",
            Name = "Abtahi Tajwar",
            Email = "abtahitajwar@gmail.com"

        };
        return [user];
    }
}
