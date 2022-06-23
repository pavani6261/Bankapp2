using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public string AccountSubType { get; set; }
        public float InterestRate { get; set; }
    }
}