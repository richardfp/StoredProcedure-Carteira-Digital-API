using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity;
using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Settings;
using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;
using Dapper;
using Microsoft.AspNetCore.Connections;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

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

        public async Task SacarSaldoContaUsuario(SaqueEntity entity)
        {
            try
            {
                var session = _connectionSettings.CreateConnection();

                var procedureName = "sp_Withdraw";

                var parameters = new DynamicParameters();

                parameters.Add("@UserId", entity.UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PasswordHash", entity.HashPassword, DbType.String, ParameterDirection.Input, size: 100);
                parameters.Add("@Amount", entity.UserId, DbType.Decimal, ParameterDirection.Input);

                await session.QueryFirstOrDefaultAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        public async Task<(List<TransactionLog> logs, string statusMessage)> Transactionlog(int transactionId)
        {
            try
            {
                var logs = new List<TransactionLog>();
                string statusMessage = string.Empty;

                using (var connection = _connectionSettings.CreateConnection())
                {
                    using (var command = new SqlCommand("sp_GetTransactionLogsByUserId", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", transactionId);

                        var statusParam = new SqlParameter("@StatusMessage", SqlDbType.NVarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(statusParam);


                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                logs.Add(new TransactionLog
                                {
                                    TransactionId = reader.GetInt32(reader.GetOrdinal("TransactionId")),
                                    Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                                    Type = reader.GetString(reader.GetOrdinal("Type")),
                                    TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                                });
                            }
                        }

                        // Captura a mensagem de status
                        statusMessage = statusParam.Value.ToString();                   
                    }

                    return (logs, statusMessage);
                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        public async Task<SaldoEntity> VerificarSaldoUsuario(SaldoEntity saldoEntity)
        {
            try
            {
                var session = _connectionSettings.CreateConnection();

                var procedureName = "sp_GetAccountBalance";

                var parameters = new DynamicParameters();

                parameters.Add("@UserId", saldoEntity.UserId, dbType: DbType.Int32, ParameterDirection.Input);
                parameters.Add("@PasswordHash", saldoEntity.PasswordHash, DbType.String, ParameterDirection.Input, size: 100);
                parameters.Add("@Balance", saldoEntity.Balance, dbType: DbType.Decimal, ParameterDirection.Output);

                await session.QueryFirstOrDefaultAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);

                saldoEntity.Balance = parameters.Get<Decimal>("@Balance");

                return saldoEntity;

            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        
    }
}
