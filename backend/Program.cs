using backend.data;
using backend.classes;
using backend.repositories;

DotNetEnv.Env.Load("../.env");

var database = new Database();

await using var connection = await database.GetConnection();

var userRepository = new UserRepositories(database);

var user = new User(1, "Test User", "test@example.com", "test_password");

await userRepository.CreateUser(user);

Console.WriteLine("Database connected!");