using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class SavingAccount
    {
        public int SavingAccountId { get; set; }
        public int CustAccountId { get; set; }
        public CustAccount CustAccount { get; set; }
        public decimal Balance { get; set; }
        public long TransferLimit { get; set; }
        public string BranchCode { get; set; }
    }
}