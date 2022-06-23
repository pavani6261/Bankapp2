using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class SavingAccTxn
    {
        public int SavingAccTxnId { get; set; }
        public int SavingAccountId { get; set; }
        public SavingAccount SavingAccount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TxnDate { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }
        public string TxnDetails { get; set; } //withdraw or deposit
    }
}