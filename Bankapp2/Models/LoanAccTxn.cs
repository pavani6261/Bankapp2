using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class LoanAccTxn
    {
        public int LoanAccTxnId { get; set; }
        public int LoanAccountId { get; set; }
        public LoanAccount LoanAccount { get; set; }
        public DateTime EmiDate { get; set; }
        public decimal EmiAmount { get; set; }
        public string Status { get; set; }
        public decimal PendingAmount { get; set; }
    }
}