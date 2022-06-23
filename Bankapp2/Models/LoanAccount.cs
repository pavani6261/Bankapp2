using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class LoanAccount
    {
        public int LoanAccountId { get; set; }
        public int CustAccountId { get; set; }
        public CustAccount CustAccount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string BranchCode { get; set; }
        public decimal RateOfInterest { get; set; }
        public int LoanDuration { get; set; }
        public decimal LoanAmount { get; set; }
    }
}