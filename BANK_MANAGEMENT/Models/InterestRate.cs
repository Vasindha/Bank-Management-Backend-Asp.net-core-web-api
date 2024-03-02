using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT.Models
{
    [Keyless]
    public class InterestRates
    {
        public double InterestRate { get; set; }
        public int AccountType { get; set; }
    }
}
