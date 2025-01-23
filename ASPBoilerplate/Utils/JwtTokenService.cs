using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ASPBoilerplate.Configurations;
using System.Diagnostics;
using ASPBoilerplate.Shared.JwtToken;

public static class JwtTokenService
{
    public static string GenerateToken(JwtTokenPayload data)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, data.UserId),
            new Claim(JwtRegisteredClaimNames.UniqueName, data.Email),
            new Claim(ClaimTypes.Role, data.Role ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.AddHours(JwtTokenSettings.ExpireInHour).ToString(), ClaimValueTypes.Integer64) // Issued at time
        };
        if (JwtTokenSettings.Secret == null)
        {
            throw new Exception("Please provide valid jwt secret");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: JwtTokenSettings.Issuer,
            audience: JwtTokenSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(JwtTokenSettings.ExpireInHour),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static JwtTokenPayload DecodeAndValidateToken(string token)
    {
        if (JwtTokenSettings.Secret == null) throw new Exception("Please provide valid jwt secret");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(JwtTokenSettings.Secret);

        // Define token validation parameters
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = JwtTokenSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtTokenSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Optional: Avoid default 5-minute clock skew
        };

        try
        {
            // Validate the token and return the claims
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Additional check for the algorithm
            if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
            {
                throw new SecurityTokenException("Invalid token algorithm");
            }


            return new JwtTokenPayload()
            {
                UserId = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                Email = principal.FindFirst(ClaimTypes.Name)!.Value,
                Role = principal.FindFirst(ClaimTypes.Role)?.Value
            };
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException($"Token validation failed: {ex.Message}");
        }
    }

    public static JwtTokenPayload DecodeTokenWithoutValidation(string token)
    {
        Console.WriteLine($"Decoding token: {token}");
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var payload = new JwtTokenPayload
        {
            UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? throw new Exception("UserId not found"),
            Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value ?? throw new Exception("Email not found"),
            Role = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role")?.Value
        };

        return payload;
    }
}