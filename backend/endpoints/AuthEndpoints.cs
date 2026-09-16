using backend.models;
using backend.security;
using System.Security.Claims;

namespace backend.endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (
                LoginRequest request,
                AuthService authService,
                HttpContext httpContext) =>
            {
                var token = await authService.Login(
                    request.Email,
                    request.Password
                );

                if (token == null)
                {
                    return Results.Unauthorized();
                }

                httpContext.Response.Cookies.Append(
                    "access_token",
                    token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)
                    }
                );

                return Results.Ok();
            });

            app.MapPost("/auth/signup", async (
                SignupRequest request,
                SignupService signupService) =>
            {
                var success = await signupService.Signup(
                    request.Name,
                    request.Email,
                    request.Password
                );

                if (!success)
                {
                    return Results.Conflict("Email already exists.");
                }

                return Results.Ok("User created successfully.");
            });

            app.MapGet("/auth/me", (ClaimsPrincipal user) =>
            {
                var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? user.FindFirst("sub")?.Value;

                var email = user.FindFirst(ClaimTypes.Email)?.Value
                    ?? user.FindFirst("email")?.Value;

                return Results.Ok(new
                {
                    id,
                    email
                });
            })
            .RequireAuthorization();

            app.MapPost("/auth/logout", (HttpContext httpContext) =>
            {
                httpContext.Response.Cookies.Delete("access_token");

                return Results.Ok();
            });
        }
    }
}