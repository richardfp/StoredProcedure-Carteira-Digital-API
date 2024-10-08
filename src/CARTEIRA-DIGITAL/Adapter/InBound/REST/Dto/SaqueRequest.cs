namespace CARTEIRA_DIGITAL.Adapter.InBound.REST.Dto
{
    public class SaqueRequest
    {
        public int UserId { get; set; }
        public string HashPassword { get; set; }
        public Decimal ValorDoSaque { get; set; }
    }
}
