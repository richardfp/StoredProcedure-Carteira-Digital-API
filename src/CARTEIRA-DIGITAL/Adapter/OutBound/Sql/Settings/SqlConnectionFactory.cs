using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;
using System.Data.SqlClient;

namespace CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Settings
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void Dispose()
        {
            // Implementar lógica de descarte, se necessário.
        }
    }
}
