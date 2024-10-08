using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity;
using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Mappers;
using CARTEIRA_DIGITAL.Domain.Core.DomainModel;
using CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound;

namespace CARTEIRA_DIGITAL.Domain.Application.UseCase
{
    public class CarteiraUseCase : ICarteiraUseCase
    {
        private readonly ICarteiraRepository _carteiraRepository;

        public CarteiraUseCase(ICarteiraRepository carteiraRepository)
        {
            _carteiraRepository = carteiraRepository;
        }

        public async Task<(List<TransactionLog> logs, string statusMessage)> UCTransactionlog(int transactionId)
        {
            try
            {
                var returns = await _carteiraRepository.Transactionlog(transactionId);

                return returns;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<SaldoModel> UCConsultarSaldo(SaldoModel model)
        {
            try
            {
                var entity = MappingEntity.ModelToEntitySaldo(model);

                var repository = await _carteiraRepository.VerificarSaldoUsuario(entity);

                return new SaldoModel
                {
                    UserId = repository.UserId,
                    PasswordHash = repository.PasswordHash,
                    Balance = repository.Balance,
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UCCriarUsuario(UserModel Model)
        {
            try
            {
                var entity = MappingEntity.ModelToEntityUser(Model);

                await _carteiraRepository.CriarContaUsuario(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UCSaqueSaldoConta(SaqueModel model)
        {
            try
            {
                var entity = MappingEntity.ModelToEntutySaque(model);

                await _carteiraRepository.SacarSaldoContaUsuario(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
