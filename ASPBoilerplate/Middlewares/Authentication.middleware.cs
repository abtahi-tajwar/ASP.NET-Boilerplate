namespace ASPBoilerplate.Middlewares;

public class AuthenticationMiddleware
{
    RequestDelegate _next;
    public AuthenticationMiddleware (RequestDelegate next) {
        _next = next;
    }
    public async Task InvokeAsync (HttpContext context) {
        Console.WriteLine("Inside Authentication middlware");
        await _next(context);
    }
}


public static class AuthenticationMiddlewareExtensions {
    public static IApplicationBuilder UseAppAuthentication (this IApplicationBuilder builder) {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}
