using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity;

namespace CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound
{
    public interface ICarteiraRepository
    {
        public Task CriarContaUsuario(UserEntity entity);
        public Task<SaldoEntity> VerificarSaldoUsuario(SaldoEntity saldoEntity);
        public Task SacarSaldoContaUsuario(SaqueEntity entity);

        public Task<(List<TransactionLog> logs, string statusMessage)> Transactionlog(int transactionId);
    }
}
