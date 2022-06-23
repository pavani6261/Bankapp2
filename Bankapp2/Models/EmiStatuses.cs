using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class EmiStatuses
    {
        public int NoOfCustomersPaidLastMonth { get; set; }
        public int NoOfCustomersPendingLastMonth { get; set; }
        public int NoOfCustomersPaidCurrentMonth { get; set; }
        public int NoOfCustomersPendingCurrentMonth { get; set; }
    }
}