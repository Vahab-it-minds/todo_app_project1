using backend.models;
using backend.security;
using backend.repositories;
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

            app.MapGet("/auth/me", async (
                ClaimsPrincipal claims,
                UserRepository userRepository) =>
            {
                var idValue = claims
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? claims.FindFirst("sub")?.Value;

                if (!int.TryParse(idValue, out var userId))
                {
                    return Results.Unauthorized();
                }

                var user = await userRepository.GetUserById(userId);

                if (user == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new
                {
                    user.Id,
                    user.Name,
                    user.Email
                });
            })
            .RequireAuthorization();

            app.MapPut("/users/me", async (
                UpdateProfileRequest request,
                ClaimsPrincipal claims,
                UserRepository userRepository) =>
            {
                var idValue =
                    claims.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? claims.FindFirst("sub")?.Value;

                if (!int.TryParse(idValue, out var userId))
                {
                    return Results.Unauthorized();
                }

                var name = request.Name.Trim();
                var email = request.Email.Trim();

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(email))
                {
                    return Results.BadRequest(
                        "Name and email are required."
                    );
                }

                var emailExists =
                    await userRepository.EmailExists(email, userId);

                if (emailExists)
                {
                    return Results.Conflict(
                        "Email already belongs to another account."
                    );
                }

                await userRepository.UpdateProfile(
                    userId,
                    name,
                    email
                );

                var updatedUser =
                    await userRepository.GetUserById(userId);

                return Results.Ok(updatedUser);
            })
            .RequireAuthorization();

            app.MapPut("/users/me/password", async (
                ChangePasswordRequest request,
                ClaimsPrincipal claims,
                UserRepository userRepository,
                PasswordHasherService passwordHasherService) =>
            {
                var idValue =
                    claims.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? claims.FindFirst("sub")?.Value;

                if (!int.TryParse(idValue, out var userId))
                {
                    return Results.Unauthorized();
                }

                if (string.IsNullOrWhiteSpace(request.CurrentPassword) ||
                    string.IsNullOrWhiteSpace(request.NewPassword))
                {
                    return Results.BadRequest(
                        "Current password and new password are required."
                    );
                }

                var currentPasswordHash =
                    await userRepository.GetPasswordHashByUserId(userId);

                if (currentPasswordHash == null)
                {
                    return Results.NotFound();
                }

                var passwordCorrect =
                    passwordHasherService.VerifyPassword(
                        request.CurrentPassword,
                        currentPasswordHash
                    );

                if (!passwordCorrect)
                {
                    return Results.BadRequest(
                        "Current password is incorrect."
                    );
                }

                var newPasswordHash =
                    passwordHasherService.HashPassword(
                        request.NewPassword
                    );

                await userRepository.UpdatePassword(
                    userId,
                    newPasswordHash
                );

                return Results.Ok();
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