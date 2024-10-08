namespace CARTEIRA_DIGITAL.Domain.Core.DomainModel
{
    public class SaqueModel
    {
        public int UserId { get; set; }
        public string HashPassword { get; set; }
        public Decimal ValorDoSaque { get; set; }
    }
}
