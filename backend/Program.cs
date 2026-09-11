using backend.data;
using backend.classes;
using backend.repositories;
using backend.security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.models;

DotNetEnv.Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Database>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TodoRepository>();

builder.Services.AddScoped<PasswordHasherService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SignupService>();





builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET")!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            ),

            ValidateIssuer = true,
            ValidIssuer = "todo-api",

            ValidateAudience = true,
            ValidAudience = "todo-client",

            ValidateLifetime = true
        };
    });


builder.Services.AddAuthorization();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/protected", () => "You are authenticated!")
    .RequireAuthorization();


app.MapPost("/auth/login", async (LoginRequest request, AuthService authService) =>
{
    var token = await authService.Login(request.Email, request.Password);

    if (token == null)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new { token });
});


app.MapPost("/auth/signup", async (SignupRequest request, SignupService signupService) =>
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

app.Run();