using backend.data;
using backend.classes;
using backend.repositories;

DotNetEnv.Env.Load("../.env");

var database = new Database();

await using var connection = await database.GetConnection();

var userRepository = new UserRepositories(database);



var foundUser = await userRepository.GetUserById(2);

if (foundUser != null)
{
    Console.WriteLine($"User found: {foundUser.Name}");
    Console.WriteLine($"Email: {foundUser.Email}");
}
else
{
    Console.WriteLine("User not found");
}

var userToUpdate = new User(2, "Updated User", "updated@example.com", "updated_password");

await userRepository.UpdateUser(userToUpdate);

var updatedUser = await userRepository.GetUserById(2);

if (updatedUser != null)
{
    Console.WriteLine($"User updated: {updatedUser.Name}");
    Console.WriteLine($"Email: {updatedUser.Email}");
}
else
{
    Console.WriteLine("Failed to update user");
}

Console.WriteLine("Database connected!");