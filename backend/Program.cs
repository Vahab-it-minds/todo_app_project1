using backend.data;
using backend.classes;
using backend.repositories;

DotNetEnv.Env.Load("../.env");

var database = new Database();

await using var connection = await database.GetConnection();

var userRepository = new UserRepositories(database);



Console.WriteLine("Database connected!");