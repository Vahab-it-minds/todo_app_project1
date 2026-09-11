using backend.classes;
using backend.repositories;

namespace backend.security
{
    public class SignupService
    {
        private readonly UserRepository userRepository;
        private readonly PasswordHasherService passwordHasherService;

        public SignupService(
            UserRepository userRepository,
            PasswordHasherService passwordHasherService)
        {
            this.userRepository = userRepository;
            this.passwordHasherService = passwordHasherService;
        }

        public async Task<bool> Signup(
            string name,
            string email,
            string password)
        {
            var emailExists = await userRepository.EmailExists(email);

            if (emailExists)
            {
                return false;
            }

            var hashedPassword =
                passwordHasherService.HashPassword(password);

            var user = new User(
                0,
                name,
                email,
                hashedPassword
            );

            await userRepository.CreateUser(user);

            return true;
        }
    }
}