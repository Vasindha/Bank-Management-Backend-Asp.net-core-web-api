namespace BANK_MANAGEMENT.DTO
{
    public class TransactionDTO
    {
        public decimal AccountNumber { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal? ReceiverAccountNumber { get; set; }
        public string? TransactionDescription { get; set; }
        public int TansactionType { get; set; }
    }
}
