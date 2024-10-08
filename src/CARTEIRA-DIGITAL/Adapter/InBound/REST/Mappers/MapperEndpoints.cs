using CARTEIRA_DIGITAL.Adapter.InBound.REST.Dto;
using CARTEIRA_DIGITAL.Domain.Core.DomainModel;

namespace CARTEIRA_DIGITAL.Adapter.InBound.REST.Mappers
{
    public static class MapperEndpoints
    {
        public static UserModel DtoToModel(UserRequest request)
        {
            return new UserModel { Email = request.Email, Nome = request.Nome, Senha = request.Senha };
        }
    }
}
