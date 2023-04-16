using Eventos.IO.Domain.INTERFACES;

namespace Eventos.IO.Services.Api.Middlewares;

public class SwaggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUser _user;

    public SwaggerMiddleware(RequestDelegate next, IUser user)
    {
        _next = next;
        _user = user;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger") && _user.IsAuthenticated())
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return;
        }

        await _next.Invoke(context);
    }
}

public static class SwaggerMiddlewareExtensions
{
    public static IApplicationBuilder UserSwaggerAuthorized(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SwaggerMiddleware>();
    }
}