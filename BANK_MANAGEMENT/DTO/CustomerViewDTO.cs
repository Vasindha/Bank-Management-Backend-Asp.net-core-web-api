using BANK_MANAGEMENT.Models;

namespace BANK_MANAGEMENT.DTO
{
    public class CustomerViewDTO
    {
        public string CustomerFirstname { get; set; } = null!;

        public string CustomerMiddletname { get; set; } = null!;

        public string CustomerLastname { get; set; } = null!;

        public string CustomerAddress { get; set; } = null!;

        public DateTime? CustomerDob { get; set; }

        public decimal? CustomerMobileno { get; set; }

        public string? CustomerEmail { get; set; }

        public decimal CustomerAadharno { get; set; }

        public  ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
