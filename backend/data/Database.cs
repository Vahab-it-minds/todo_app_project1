using Npgsql;

namespace backend.data
{
    public class Database
    {
        private readonly string connectionString =
            new NpgsqlConnectionStringBuilder
            {
                Host = Environment.GetEnvironmentVariable("DB_HOST"),
                Port = int.Parse(Environment.GetEnvironmentVariable("DB_PORT")!),
                Database = Environment.GetEnvironmentVariable("DB_NAME"),
                Username = Environment.GetEnvironmentVariable("DB_USER"),
                Password = Environment.GetEnvironmentVariable("DB_PASSWORD")
            }.ConnectionString;

        public async Task<NpgsqlConnection> GetConnection()
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            return connection;
        }
    }
}