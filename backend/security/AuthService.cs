using backend.repositories;
using backend.classes;


namespace backend.security
{
    public class AuthService
    {
        private readonly UserRepository userRepository;
        private readonly PasswordHasherService passwordHasherService;
        private readonly JwtService jwtService;

        public AuthService(UserRepository userRepository, PasswordHasherService passwordHasherService, JwtService jwtService)
        {
            this.userRepository = userRepository;
            this.passwordHasherService = passwordHasherService;
            this.jwtService = jwtService;
        }

        public async Task<string?> Login(string email, string password)
        {
            var user = await userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            var passwordCorrect = passwordHasherService.VerifyPassword(password, user.Password!);

            if (!passwordCorrect)
            {
                return null;
            }

            return jwtService.GenerateToken(user);
        }
    }
}