using System;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Mvc;
using ASPBoilerplate.Filters;
using ASPBoilerplate.Modules.Auth;
using ASPBoilerplate.Modules.User.Dtos;
using ASPBoilerplate.Modules.User.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ASPBoilerplate.Services;
using Nancy.Json;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace ASPBoilerplate.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
    [HttpGet("list", Name = "ListUsers")]
    [AppAuthorize("Admin")]
    public IEnumerable<UnrestrictedUserEntity> List(AppDbContext context)
    {
        var users = context.UnrestrictedUsers.ToList();
        return users;
    }

}

[ApiController]
[Route("admin/user")]
public class UserControllerAdmin : ControllerBase
{
    private IDistributedCache _cache;
    public UserControllerAdmin(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    [HttpGet("list", Name = "ListUsersAdmin")]
    [AppAuthorize("Admin")]
    public IResult List(AppDbContext context)
    {
        var users = context.RestrictedUsers.ToList();
        // var _cacheService = new CacheService(_cache);
        // var users = _cacheService.GetOrCreate<List<RestrictedUserEntity>>(
        //     "user",
        //     () => context.RestrictedUsers.ToList()
        // );
        return CustomResponse.Ok(users);

    }
    [HttpPost("create", Name = "CreateUserAdmin")]
    public IResult CreateUserAdmin(CreateUserAdminDto body, AppDbContext context)
    {
        var service = new UserService(context);
        RestrictedUserEntity newUser = service.RegisterUserEmailAdmin(body);
        return CustomResponse.Ok(newUser, "User Registered");
    }

    [HttpGet("my-profile", Name = "MyProfileAdmin")]
    public IResult GetMyProfile(AppDbContext context)
    {
        try
        {
            var service = new UserService(context);
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Standard claim for user ID
            var Profile = service.GetUserProfile(UserId);
            if (Profile == null)
            {
                return CustomResponse.BadRequest("Can't extract profile information of this user");
            }
            return CustomResponse.Ok(Profile);
        }
        catch (Exception e)
        {
            return CustomResponse.BadRequest(e.Message);
        }
    }
}