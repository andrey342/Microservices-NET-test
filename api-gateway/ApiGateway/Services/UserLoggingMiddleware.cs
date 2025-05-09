namespace ApiGateway.Services;

public class UserLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public UserLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userName = context.User.Identity.Name;
            context.Items["UserName"] = userName;
        }

        await _next(context);
    }
}

