namespace CARTEIRA_DIGITAL.Domain.Core.DomainModel
{
    public class SaldoModel
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
        public Decimal Balance { get; set; }
    }
}
