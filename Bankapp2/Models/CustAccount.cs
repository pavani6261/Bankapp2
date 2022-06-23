using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Bankapp2.Models;
namespace Bankapp2.Models
{
    public class CustAccount
    {
        public int CustAccountId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
    }
}