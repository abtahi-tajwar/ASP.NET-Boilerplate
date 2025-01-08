using System;
using System.Text;
using ASPBoilerplate.Modules.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ASPBoilerplate.Configurations;


public class AuthSettings
{
    public static List<(string, USER_ROLES[])> RoleMapping = new() {
        ("AdminOnly", [USER_ROLES.ADMIN]),
        ("SuperAdmin", [USER_ROLES.SUPER_ADMIN]),
        ("Admin", [USER_ROLES.ADMIN, USER_ROLES.SUPER_ADMIN]),
    };
    public static void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtTokenSettings.Issuer,
                    ValidAudience = JwtTokenSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Secret!))
                };
            });


        builder.Services.AddAuthorization(options =>
        {
            foreach(var role in RoleMapping) {
                options.AddPolicy(role.Item1, policy => {
                    string[] roleStrings = role.Item2.Select(role => role.ToString()).ToArray();
                    policy.RequireRole(roleStrings);
                });
            }
        });
    }
}
