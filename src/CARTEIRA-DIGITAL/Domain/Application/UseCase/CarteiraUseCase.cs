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

        public async Task UCCriarUsuario(UserModel Model)
        {
            try
            {
                var entity = MappingEntity.ModelToEntity(Model);

                await _carteiraRepository.CriarContaUsuario(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
