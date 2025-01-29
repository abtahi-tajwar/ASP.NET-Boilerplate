using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace ASPBoilerplate.Services;

[ApiController]
[Route("/signin-google")]
public class GoogleAuthController : ControllerBase
{
    private readonly GoogleAuthService _service;
    public GoogleAuthController(GoogleAuthService service)
    {
        _service = service;
    }
    [HttpGet("")]
    public async Task<IResult> GoogleLoginCallback([FromQuery] string code)
    {
        try
        {
            Console.WriteLine("This is hitting");

            if (string.IsNullOrEmpty(code)) return CustomResponse.BadRequest("Authorization code is missing");

            // Exchange the code for an access token
            var tokenResponse = await _service.ExchangeCodeForToken(code);

            if (tokenResponse == null) return CustomResponse.Unauthorized();

            // Get user info from Google
            var googleUser = await _service.GetGoogleUserInfo(tokenResponse.access_token);
            
            // Need to update here to handle database saving thing

            return CustomResponse.Ok(googleUser);
        } catch (Exception e) {
            return CustomResponse.Ok(e.Message);
        }
    }
}