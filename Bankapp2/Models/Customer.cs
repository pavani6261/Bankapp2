using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public int ZipCodeId { get; set; }
        public ZipCode ZipCode { get; set; }
    }
}