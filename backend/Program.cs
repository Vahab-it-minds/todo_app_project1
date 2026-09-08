using backend.data;

DotNetEnv.Env.Load("../.env");

var database = new Database();

await using var connection = await database.GetConnection();

Console.WriteLine("Database connected!");