using System;
using System.Collections.Generic;

namespace BANK_MANAGEMENT.Models;

public partial class Customer
{
    public decimal CustomerId { get; set; }

    public string CustomerFirstname { get; set; } = null!;

    public string CustomerMiddletname { get; set; } = null!;

    public string CustomerLastname { get; set; } = null!;

    public string CustomerAddress { get; set; } = null!;

    public DateTime? CustomerDob { get; set; }

    public decimal? CustomerMobileno { get; set; }

    public string? CustomerEmail { get; set; }

    public decimal CustomerAadharno { get; set; }

   
}
