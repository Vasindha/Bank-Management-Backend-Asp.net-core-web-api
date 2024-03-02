namespace BANK_MANAGEMENT.DTO
{
    public class AccountDto
    {
        public decimal? CustomerId { get; set; }

        public int AccountType { get; set; }

        public decimal Amount { get; set; }

        public double InterestRate { get; set; }

        public DateTime OpenDate { get; set; }

        public int AccountStatus { get; set; }
    }
}
