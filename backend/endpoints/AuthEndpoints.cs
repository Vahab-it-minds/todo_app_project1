using backend.models;
using backend.security;

namespace backend.endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            app.MapPost("/auth/login", async (
                LoginRequest request,
                AuthService authService) =>
            {
                var token = await authService.Login(
                    request.Email,
                    request.Password
                );

                if (token == null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(new { token });
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
        }
    }
}