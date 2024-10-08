namespace CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity
{
    public class SaqueEntity
    {
        public int UserId { get; set; }
        public string HashPassword { get; set; }
        public Decimal ValorDoSaque { get; set; }
    }
}
