using backend.classes;
using backend.data;
using Npgsql;

namespace backend.repositories
{
    public class UserRepositories
    {
        private readonly Database database;

        public UserRepositories(Database database)
        {
            this.database = database;
        }


        public async Task CreateUser(User user)
        {
            await using var connection = await database.GetConnection();

            const string sql ="""
                INSERT INTO users (id,name, email, password)
                VALUES (@Id, @Name, @Email, @Password);
            """;

            await using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password!);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            await using var connection = await database.GetConnection();

            const string sql = """
                SELECT id, name, email
                FROM users
                WHERE ID = @id;
            """;
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", id);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var userId = reader.GetInt32(0);
                var name = reader.GetString(1);
                var email = reader.GetString(2);

                return new User(userId, name, email);
            }else
            {
                return null;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            await using var connection = await database.GetConnection();

            const string sql = """
            SELECT id, name, email
            FROM users;
            """;

            await using var command= new NpgsqlCommand(sql, connection);

            await using var reader = await command.ExecuteReaderAsync();
            
            var users = new List<User>();

            while(await reader.ReadAsync())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var email = reader.GetString(2);

                users.Add(new User(id, name, email));
            }

            return users;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            await using var connection = await database.GetConnection();

            const string sql = """
                SELECT id, name, email, password
                FROM users
                WHERE email = @email;
            """;

            await using var command = new NpgsqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("@email", email);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var userEmail = reader.GetString(2);
                var password = reader.GetString(3);

                return new User(id, name, userEmail, password);
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateUser(User user)
        {
            await using var connection = await database.GetConnection();

            const string sql = """
                UPDATE users
                SET name = @name, email = @email, password = @password
                WHERE id = @id;
            """; 

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password!);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteUser(int id)
        {
            await using var connection = await database.GetConnection();

            const string sql = """
                DELETE FROM users
                WHERE id = @id;
                """;

            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("id", id);

            await command.ExecuteNonQueryAsync();
        }
    }


}