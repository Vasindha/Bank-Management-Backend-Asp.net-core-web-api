using System;
using System.Collections.Generic;

namespace BANK_MANAGEMENT.Models;

public partial class Transaction
{
    public decimal TransactionId { get; set; }
    public decimal? ReceiverAccountNumber { get; set; }
    public decimal AccountNumber { get; set; }

    public DateTime TransactionDate { get; set; }

    public int TansactionType { get; set; }

    public decimal TransactionAmount { get; set; }

    public string? TransactionDescription { get; set; }

    //public virtual Account? AccountNumberNavigation { get; set; }
}
