using System;
using ASPBoilerplate.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPBoilerplate.Filters;

public class DevAuthorization : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the custom header "DevToken" is present in the request
        if (context.HttpContext.Request.Headers.TryGetValue("DevKey", out var devToken))
        {
            // Access the header value
            string tokenValue = devToken.ToString();

            // Perform your authorization logic here
            if (string.IsNullOrEmpty(tokenValue) || tokenValue != GeneralSettings.DeveloperKey)
            {
                // Handle unauthorized request
                context.Result = new ContentResult
                {

                    StatusCode = 401, // Set the desired status code
                    Content = "Access denied due to insufficient roles.",
                    ContentType = "application/json"

                };
            }
        }
        else
        {
            // Handle missing header
            context.Result = new ContentResult
            {

                StatusCode = 401, // Set the desired status code
                Content = "Access denied due to insufficient roles.",
                ContentType = "application/json"

            }; 
        }

        // throw new NotImplementedException();
    }
}
