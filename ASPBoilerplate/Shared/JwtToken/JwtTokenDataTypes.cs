using System;

namespace ASPBoilerplate.Shared.JwtToken;

public class JwtTokenPayload {
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public string? Role { get; set;}

}