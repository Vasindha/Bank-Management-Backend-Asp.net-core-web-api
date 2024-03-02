using System;
using System.Collections.Generic;

namespace BANK_MANAGEMENT.Models;

public partial class Account
{
    public decimal AccountNumber { get; set; }

    public decimal? CustomerId { get; set; }

    public int AccountType { get; set; }

    public decimal Amount { get; set; }

    public double InterestRate { get; set; }

    public DateTime OpenDate { get; set; }

    public int AccountStatus { get; set; }

    //public virtual Customer? Customer { get; set; }

    //public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
