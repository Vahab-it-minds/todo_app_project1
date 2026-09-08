//using backend.classes;
//User user = new User(1, "John Doe", "john.doe@example.com", "12345");
//Todo todo = new Todo(1, "Finish project", "Complete the project by the end of the week", DateTime.Now, DateTime.Now, DateTime.Now.AddDays(7), 1, "High", 5, "Work", false);

//Console.WriteLine($"User: {user.GetName()}, Email: {user.GetEmail()}, Password: {user.GetPassword()}");
//Console.WriteLine($"Todo: {todo.GetTitle()}, Description: {todo.GetDescription()}, Due Date: {todo.GetDueDate()}, Priority: {todo.GetPriority()}, Time Estimate: {todo.GetTimeEstimate()} hours, Category: {todo.GetCategory()}, Is Completed: {todo.GetIsCompleted()}");
//Console.WriteLine("Hello, World!");

using Npgsql;

var connectionString =
    "Host=localhost;Port=5432;Database=todo_db;Username=todo_user;Password=todo_password";

await using var dataSource = NpgsqlDataSource.Create(connectionString);
await using var connection = await dataSource.OpenConnectionAsync();

Console.WriteLine("Connected to PostgreSQL!");