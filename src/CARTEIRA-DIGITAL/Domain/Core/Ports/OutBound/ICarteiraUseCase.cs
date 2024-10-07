using CARTEIRA_DIGITAL.Domain.Core.DomainModel;

namespace CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound
{
    public interface ICarteiraUseCase
    {
        Task UCCriarUsuario(UserModel Model);
    }
}
