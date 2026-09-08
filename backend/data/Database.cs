using Npgsql;

namespace backend.data
{
    public class Database
    {
        private readonly string connectionString = "";

        public async Task<NpgsqlConnection> GetConnection()
        {
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            return connection;
        }
    }
}