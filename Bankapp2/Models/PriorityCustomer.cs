using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class PriorityCustomers
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public int CustAccountId { get; set; }
        public decimal Balance { get; set; }
        public PriorityCustomers(int CustomerId, string FullName, int CustAccountId, decimal Balance)
        {
            this.CustomerId = CustomerId;
            this.FullName = FullName;
            this.CustAccountId = CustAccountId;
            this.Balance = Balance;
        }
        public PriorityCustomers() { }
    }
}