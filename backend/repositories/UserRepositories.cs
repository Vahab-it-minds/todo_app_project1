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
            command.Parameters.AddWithValue("@Password", user.Password);
        
            await command.ExecuteNonQueryAsync();
        }

    }


}