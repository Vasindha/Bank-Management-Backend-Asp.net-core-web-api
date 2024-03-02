using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT.Models
{
    [Keyless]
    public class CustomerAccount
    {

        public decimal CustomerId { get; set; }
        public decimal Amount { get; set; }

        public decimal InterestRate { get; set; }

        public DateTime OpenDate { get; set; }
        public int AccountType { get; set; }
        public int AccountStatus { get; set; }
        public decimal? AccountNumber { get; set; } = null;
        public string? CustomerFirstname { get; set; } = null;

        public string CustomerMiddletname { get; set; } = null!;

        public string CustomerLastname { get; set; } = null!;

        public string CustomerAddress { get; set; } = null!;

        public DateTime? CustomerDob { get; set; }

        public decimal? CustomerMobileno { get; set; }

        public string? CustomerEmail { get; set; }

        public decimal CustomerAadharno { get; set; }
    }
}
