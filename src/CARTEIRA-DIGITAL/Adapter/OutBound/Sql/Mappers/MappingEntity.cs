using CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity;
using CARTEIRA_DIGITAL.Domain.Core.DomainModel;

namespace CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Mappers
{
    public static class MappingEntity
    {
        public static UserEntity ModelToEntityUser(UserModel model)
        {
            return new UserEntity
            {
                Email = model.Email,
                Nome = model.Nome,
                Senha = model.Senha,
            };
        }

        public static SaldoEntity ModelToEntitySaldo(SaldoModel model) 
        {
            return new SaldoEntity
            {
                Balance = model.Balance,
                PasswordHash = model.PasswordHash,
                UserId = model.UserId,
            };
        }

        public static SaqueEntity ModelToEntutySaque(SaqueModel model) 
        {
            return new SaqueEntity
            {
                UserId = model.UserId,
                HashPassword = model.HashPassword,
                ValorDoSaque = model.ValorDoSaque,
            };
        }
    }
}
