namespace CARTEIRA_DIGITAL.Adapter.OutBound.Sql.Entity
{
    public class TransactionLog
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
