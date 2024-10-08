using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity;
using CARTEIRA_DIGITAL.Domain.Core.DomainModel;

namespace CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound
{
    public interface ICarteiraUseCase
    {
        Task UCCriarUsuario(UserModel Model);

        Task<SaldoModel> UCConsultarSaldo(SaldoModel model);

        Task UCSaqueSaldoConta(SaqueModel model);

        Task<(List<TransactionLog> logs, string statusMessage)> UCTransactionlog(int transactionId);
    }
}
