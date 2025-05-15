using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure
{
    public class DataContext
    {
        private readonly string _connectionString;

        public DataContext(string connectionString)
        { _connectionString = connectionString; }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
