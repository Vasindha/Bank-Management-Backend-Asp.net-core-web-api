namespace BANK_MANAGEMENT.DTO
{
    public class CustomerDTO
    {
        public string CustomerFirstname { get; set; } = null!;

        public string CustomerMiddletname { get; set; } = null!;

        public string CustomerLastname { get; set; } = null!;

        public string CustomerAddress { get; set; } = null!;

        public DateTime? CustomerDob { get; set; }

        public decimal? CustomerMobileno { get; set; }

        public string? CustomerEmail { get; set; }

        public decimal CustomerAadharno { get; set; }
    }
}
