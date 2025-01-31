using Microsoft.AspNetCore.Mvc;

namespace ASPBoilerplate.Services;

[ApiController]
[Route("/signin-facebook")]
public class FacebookAuthController {
    public async Task<IResult> AuthorizeUserToken()
    {
        return CustomResponse.Ok("OK");
    }
}