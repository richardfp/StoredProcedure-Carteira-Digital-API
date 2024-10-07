using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity;

namespace CARTEIRA_DIGITAL.Domain.Core.Ports.OutBound
{
    public interface ICarteiraRepository
    {
        Task CriarContaUsuario(UserEntity entity);
    }
}
