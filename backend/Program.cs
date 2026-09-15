using backend.data;
using backend.repositories;
using backend.security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using backend.endpoints;

DotNetEnv.Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Database>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TodoRepository>();

builder.Services.AddScoped<PasswordHasherService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<SignupService>();



builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

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

app.MapTodoEndpoints();
app.MapAuthEndpoints();

app.Run();