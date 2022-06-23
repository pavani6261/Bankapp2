using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Bankapp2.Models;
namespace Bankapp2.Controllers
{
    public class CustAccountsController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/CustAccounts
        public IQueryable<CustAccount> GetCustAccounts()
        {
            return db.CustAccounts;
        }
        public List<CustAccount> GetCustAccountsByCustomerId(int id)
        {
            List<CustAccount> custAccounts = new List<CustAccount>();
            foreach (CustAccount custAcc in db.CustAccounts)
            {
                if (custAcc.CustomerId == id)
                {
                    custAccounts.Add(custAcc);
                }
            }
            return custAccounts;
        }
        //check this by enabling migration again and adding and update database;
        public List<CustomerAccountInfo> GetCustomerAccountInfoByCustomerId(int id)
        {
            List<CustomerAccountInfo> custAccInfoList = new List<CustomerAccountInfo>();
            foreach (CustAccount custAcc in db.CustAccounts.ToList())
            {
                if (custAcc.CustomerId == id)
                {
                    foreach (AccountType accType in db.AccountTypes.ToList())
                    {
                        if (custAcc.AccountTypeId == accType.AccountTypeId)
                        {
                            custAccInfoList.Add(new CustomerAccountInfo(custAcc.CustAccountId, accType.AccountTypeName, accType.AccountSubType));
                        }
                    }
                }
            }
            return custAccInfoList;
        }
        public CustAccount GetCustAccountByCustAccountId(int id)
        {
            CustAccount custaccount = db.CustAccounts.Find(id);
            if (custaccount == null)
            {
                return new CustAccount();
            }
            return custaccount;
        }
        // GET api/CustAccounts/5
        [ResponseType(typeof(CustAccount))]
        public IHttpActionResult GetCustAccount(int id)
        {
            CustAccount custaccount = db.CustAccounts.Find(id);
            if (custaccount == null)
            {
                return NotFound();
            }
            return Ok(custaccount);
        }
        // PUT api/CustAccounts/5
        public IHttpActionResult PutCustAccount(int id, CustAccount custaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != custaccount.CustAccountId)
            {
                return BadRequest();
            }
            db.Entry(custaccount).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        // POST api/CustAccounts
        [ResponseType(typeof(CustAccount))]
        public IHttpActionResult PostCustAccount(CustAccount custaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.CustAccounts.Add(custaccount);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = custaccount.CustAccountId }, custaccount);
        }
        // DELETE api/CustAccounts/5
        [ResponseType(typeof(CustAccount))]
        public IHttpActionResult DeleteCustAccount(int id)
        {
            CustAccount custaccount = db.CustAccounts.Find(id);
            if (custaccount == null)
            {
                return NotFound();
            }
            db.CustAccounts.Remove(custaccount);
            db.SaveChanges();
            return Ok(custaccount);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CustAccountExists(int id)
        {
            return db.CustAccounts.Count(e => e.CustAccountId == id) > 0;
        }
    }
}