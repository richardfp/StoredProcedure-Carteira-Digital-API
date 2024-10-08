using System.Data.SqlClient;

namespace CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
