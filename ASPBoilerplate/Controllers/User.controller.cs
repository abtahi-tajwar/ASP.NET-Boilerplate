using System;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Mvc;

namespace ASPBoilerplate.Controllers;

[ApiController]
[Route("/user")]
public class User : ControllerBase
{
    [HttpGet("list", Name = "ListUsers")]
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
