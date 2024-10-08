    namespace CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity
{
    public class SaldoEntity
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
        public Decimal Balance { get; set; }
    }
}
