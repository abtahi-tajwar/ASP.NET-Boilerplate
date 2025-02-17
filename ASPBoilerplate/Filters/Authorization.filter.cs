using System;
using ASPBoilerplate.Configurations;
using ASPBoilerplate.Modules.Auth;
using ASPBoilerplate.Modules.User;
using ASPBoilerplate.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPBoilerplate.Filters;

public class AppAuthorize : Attribute, IAuthorizationFilter
{

    public static string _role;

    public AppAuthorize(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {


        var bearer = context.HttpContext.Request.Headers.Authorization;
        var token = bearer.ToString().Replace("Bearer ", "").Trim();
        var serviceProvider = context.HttpContext.RequestServices;
        var scope = serviceProvider.CreateScope();

        var authService = scope.ServiceProvider.GetRequiredService<AuthService>();

        var ValidationResponse = authService.ValidateToken(token);

        var AllowedRoles = AuthSettings.RoleMapping.Find(role => role.Item1 == _role);
        var RoleEnum = Enum.Parse<USER_ROLES>(_role.ToUpper());

        if (!ValidationResponse.IsValid) {
            context.Result = new ContentResult
            {

                StatusCode = 401, // Set the desired status code
                Content = "Access denied due to invalid token.",
                ContentType = "application/json"

            };

            return;
        }
        if (!Array.Exists(AllowedRoles.Item2, element => element == RoleEnum))
        {

            context.Result = new ContentResult
            {

                StatusCode = 401, // Set the desired status code
                Content = "Access denied due to insufficient roles.",
                ContentType = "application/json"

            };

            return;
        }
    }
}
