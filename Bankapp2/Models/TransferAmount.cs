using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class TransferAmount
    {
        public int SenderSavingAccountId { get; set; }
        public int RecieverSavingAccountId { get; set; }
        public string Remarks { get; set; }
        public decimal Amount { get; set; }
    }
}