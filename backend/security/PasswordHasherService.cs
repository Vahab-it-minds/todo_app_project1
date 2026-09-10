using Microsoft.AspNetCore.Identity;

namespace backend.security
{
    public class PasswordHasherService
    {
        private readonly PasswordHasher<object> hasher = new();

        public string HashPassword(string password)
        {
            return hasher.HashPassword(null!, password);
        }


        public bool VerifyPassword(string password, string hashedPassword)
        {
            var result = hasher.VerifyHashedPassword(null!, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}