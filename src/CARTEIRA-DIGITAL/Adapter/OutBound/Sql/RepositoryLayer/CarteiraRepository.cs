using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity;
using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Settings;
using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace CARTEIRA_DIGITAL.Adapter.OutBound.Sql.RepositoryLayer
{
    public class CarteiraRepository : ICarteiraRepository
    {
        private readonly ISqlConnectionFactory _connectionSettings;

        public CarteiraRepository(ISqlConnectionFactory sqlConnectionSettings)
        {
            _connectionSettings = sqlConnectionSettings;
        }

        public async Task CriarContaUsuario(UserEntity entity)
        {
            try
            {
                var session = _connectionSettings.CreateConnection();

                var procedureName = "sp_CreateUserAccount";

                var parameters = new DynamicParameters();

                parameters.Add("@Name", entity.Nome, DbType.String, ParameterDirection.Input, size: 100);
                parameters.Add("@Email", entity.Email, DbType.String, ParameterDirection.Input, size: 100);
                parameters.Add("@PasswordHash", entity.Senha, DbType.String, ParameterDirection.Input, size: 100);

                await session.QueryFirstOrDefaultAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

            }
            catch (SqlException ex)
            {

                throw;
            }
        }
    }
}
