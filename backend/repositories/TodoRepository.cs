using backend.data;
using backend.classes;
using Npgsql;

namespace backend.repositories
{
    public class TodoRepository
    {
        private readonly Database database;

        public TodoRepository(Database database)
        {
            this.database = database;
        }

        public async Task CreateTodo(Todo todo)
        {
            await using var connection = await database.GetConnection();

            const string sql = """
            INSERT INTO todos (
            id, title, description, created_at, updated_at, due_date, user_id, priority, time_estimate, category, is_completed
            )
            VALUES(
            @Id, @Title, @Description, @CreatedAt, @UpdatedAt, @DueDate, @UserId, @Priority, @TimeEstimate, @Category, @IsCompleted
            );
            """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", todo.Id);
            command.Parameters.AddWithValue("@Title", todo.Title);
            command.Parameters.AddWithValue("@Description", todo.Description);
            command.Parameters.AddWithValue("@CreatedAt", todo.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", todo.UpdatedAt);
            command.Parameters.AddWithValue("@DueDate", todo.DueDate);
            command.Parameters.AddWithValue("@UserId", todo.UserId);
            command.Parameters.AddWithValue("@Priority", todo.Priority.ToString());
            command.Parameters.AddWithValue("@TimeEstimate", todo.TimeEstimate);
            command.Parameters.AddWithValue("@Category", todo.Category);
            command.Parameters.AddWithValue("@IsCompleted", todo.IsCompleted);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<Todo?> GetTodoById(int id)
        {
            await using var connection = await database.GetConnection();

            const string sql = """
            SELECT id, title, description, created_at, updated_at, due_date, user_id, priority, time_estimate, category, is_completed
            FROM todos
            WHERE id = @Id
            """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", id);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var todoId = reader.GetInt32(0);
                var title = reader.GetString(1);
                var description = reader.GetString(2);
                var createdAt = reader.GetDateTime(3);
                var updatedAt = reader.GetDateTime(4);
                var dueDate = reader.GetDateTime(5);
                var userId = reader.GetInt32(6);
                var priority = Enum.Parse<Priority>(reader.GetString(7));
                var timeEstimate = reader.GetInt32(8);
                var category = reader.GetString(9);
                var isCompleted = reader.GetBoolean(10);

                return new Todo(todoId, title, description, createdAt, updatedAt, dueDate, userId, priority, timeEstimate, category, isCompleted);
            }

            return null;
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            await using var connection = await database.GetConnection();

            const string sql = """
            SELECT id, title, description, created_at, updated_at, due_date, user_id, priority, time_estimate, category, is_completed
            FROM todos
            """;

            await using var command = new NpgsqlCommand(sql, connection);

            await using var reader = await command.ExecuteReaderAsync();

            var todos = new List<Todo>();

            while (await reader.ReadAsync())
            {
                var id = reader.GetInt32(0);
                var title = reader.GetString(1);
                var description = reader.GetString(2);
                var createdAt = reader.GetDateTime(3);
                var updatedAt = reader.GetDateTime(4);
                var dueDate = reader.GetDateTime(5);
                var userId = reader.GetInt32(6);
                var priority = Enum.Parse<Priority>(reader.GetString(7));
                var timeEstimate = reader.GetInt32(8);
                var category = reader.GetString(9);
                var isCompleted = reader.GetBoolean(10);

                todos.Add(new Todo(id,title, description, createdAt, updatedAt, dueDate, userId, priority, timeEstimate, category, isCompleted));
            }

            return todos;
        }

        public async Task UpdateTodo(Todo todo)
        {
            await using var connection = await database.GetConnection();
            
            var updatedAt = DateTime.Now;

            const string sql = """
            UPDATE todos
            SET title = @Title,
                description = @Description,
                updated_at = @UpdatedAt,
                due_date = @DueDate,
                priority = @Priority,
                time_estimate = @TimeEstimate,
                category = @Category,
                is_completed = @IsCompleted
            WHERE id = @Id
            """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", todo.Id);
            command.Parameters.AddWithValue("@Title", todo.Title);
            command.Parameters.AddWithValue("@Description", todo.Description);
            command.Parameters.AddWithValue("@UpdatedAt", updatedAt);
            command.Parameters.AddWithValue("@DueDate", todo.DueDate);
            command.Parameters.AddWithValue("@Priority", todo.Priority.ToString());
            command.Parameters.AddWithValue("@TimeEstimate", todo.TimeEstimate);
            command.Parameters.AddWithValue("@Category", todo.Category);
            command.Parameters.AddWithValue("@IsCompleted", todo.IsCompleted);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteTodo(int id)
        {
            await using var connection = await database.GetConnection();

            const string sql = """
            DELETE FROM todos
            WHERE id = @Id
            """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}