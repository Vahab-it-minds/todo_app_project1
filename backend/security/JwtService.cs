using backend.classes;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace backend.security
{
    public class JwtService
    {
        private readonly JwtSecurityTokenHandler tokenHandler = new();
        private readonly string secretKey = Environment.GetEnvironmentVariable("JWT_SECRET")!;
        
        private readonly SymmetricSecurityKey securityKey;
        private readonly SigningCredentials signingCredentials;
    

        public JwtService()
        {
            securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey)
            );

            signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
            );
        }

        public string GenerateToken(User user)
        {
            var token = new JwtSecurityToken(
                issuer: "todo-api",
                audience: "todo-client",

                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
                );

            return tokenHandler.WriteToken(token);
        }
    }
}