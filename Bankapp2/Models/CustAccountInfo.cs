using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class CustomerAccountInfo
    {
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string AccountSubType { get; set; }
        public CustomerAccountInfo() { }
        public CustomerAccountInfo(int AccountNumber, string AccountType, string AccountSubType)
        {
            this.AccountNumber = AccountNumber;
            this.AccountSubType = AccountSubType;
            this.AccountType = AccountType;
        }
    }
}