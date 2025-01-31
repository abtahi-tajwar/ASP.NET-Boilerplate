using Microsoft.AspNetCore.Mvc;

namespace ASPBoilerplate.Services;

[ApiController]
[Route("/signin-facebook")]
public class FacebookAuthController : ControllerBase {
    private readonly FacebookAuthService _serivce;
    public FacebookAuthController (FacebookAuthService service) {
        _serivce = service;
        
    }
    [HttpPost("")]
    public async Task<IResult> AuthorizeUserToken(FacebookAuthSigninRequestPayload Body)
    {
        var Response = await _serivce.GetFacebookProfileAsync(Body.AccessToken);
        Console.WriteLine(Response);
        return CustomResponse.Ok("OK");
    }
}