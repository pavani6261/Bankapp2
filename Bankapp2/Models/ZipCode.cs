using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Bankapp2.Models
{
    public class ZipCode
    {
        public int ZipCodeId { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}