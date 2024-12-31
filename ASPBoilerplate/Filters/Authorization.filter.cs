using System;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPBoilerplate.Filters;

public class AuthorizationFilter : Attribute, IAuthorizationFilter
{

    public static string _role;

    public AuthorizationFilter(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var bearer = context.HttpContext.Request.Headers.Authorization;
        Console.WriteLine("Authorization", bearer);

        if (_role != "Admin")
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
