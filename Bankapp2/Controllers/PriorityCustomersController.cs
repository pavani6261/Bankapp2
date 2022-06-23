using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bankapp2.Models;
namespace Bankapp2.Controllers
{
    public class PriorityCustomersController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/prioritycustomers
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        // GET api/prioritycustomers/5
        //public string Get(int id)
        //{
        //    return "value";
        //}
        public List<PriorityCustomers> Get(decimal id)
        {
            //List<SavingAccount>  prioritySvAccs = new List<SavingAccount>();
            List<PriorityCustomers> priorityCustomers = new List<PriorityCustomers>();
            foreach (SavingAccount sa in db.SavingAccounts.ToList())
            {
                if (sa.Balance > id)
                {
                    foreach (CustAccount ca in db.CustAccounts.ToList())
                    {
                        if (ca.CustAccountId == sa.CustAccountId)
                        {
                            foreach (Customer cus in db.Customers.ToList())
                            {
                                if (cus.CustomerId == ca.CustomerId)
                                {
                                    priorityCustomers.Add(new PriorityCustomers(cus.CustomerId, cus.FirstName + " " + cus.LastName, ca.CustAccountId, sa.Balance));
                                }
                            }
                        }
                    }
                }
            }
            return priorityCustomers;
        }
        // POST api/prioritycustomers
        //public void Post([FromBody]string value)
        //{
        //}
        //// PUT api/prioritycustomers/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        //// DELETE api/prioritycustomers/5
        //public void Delete(int id)
        //{
        //}
    }
}