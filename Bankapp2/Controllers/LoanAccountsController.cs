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
    public class LoanAccountsController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/LoanAccounts
        public IQueryable<LoanAccount> GetLoanAccounts()
        {
            return db.LoanAccounts;
        }
        // GET api/LoanAccounts/5    //modified to take custAccounid and finds account based on loan account id
        [ResponseType(typeof(LoanAccount))]
        public IHttpActionResult GetLoanAccount(int id)
        {
            //LoanAccount loanaccount = db.LoanAccounts.Find(id);
            //if (loanaccount == null)
            //{
            //    return NotFound();
            //}
            //return Ok(loanaccount);
            foreach (LoanAccount la in db.LoanAccounts.ToList())
            {
                if (la.CustAccountId == id)
                {
                    return Ok(la);
                }
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/LoanAccounts/GetEmiStatuses")]
        public IHttpActionResult GetEmiStatuses()
        {
            EmiStatuses emistatuses = new EmiStatuses();
            int pendingCustomers = 0;
            emistatuses.NoOfCustomersPaidLastMonth = 0;
            emistatuses.NoOfCustomersPaidCurrentMonth = 0;
            string MonthNum;//= DateTime.Now.ToString("MM");
            string MonthCurrent = DateTime.Now.ToString("MM");
            int MonthNumber = DateTime.Now.Month - 1;
            if (MonthNumber == 0)
            {
                MonthNum = "12";
            }
            if (MonthNumber < 10)
            {
                MonthNum = "0" + MonthNumber.ToString();
            }
            else
            {
                MonthNum = MonthNumber.ToString();
            }
            foreach (LoanAccount la in db.LoanAccounts.ToList())
            {
                if (la.BalanceAmount > 0)
                {
                    foreach (LoanAccTxn ltxn in db.LoanAccTxns.ToList())
                    {
                        if (ltxn.LoanAccountId == la.LoanAccountId)
                        {
                            string emiDate = ltxn.EmiDate.ToString("MM");
                            if (emiDate.Equals(MonthNum))
                            {
                                emistatuses.NoOfCustomersPaidLastMonth += 1;
                            }
                            if (emiDate.Equals(MonthCurrent))
                            {
                                emistatuses.NoOfCustomersPaidCurrentMonth += 1;
                            }
                        }
                    }
                    pendingCustomers += 1;
                }
            }
            emistatuses.NoOfCustomersPendingCurrentMonth = pendingCustomers - emistatuses.NoOfCustomersPaidCurrentMonth;
            emistatuses.NoOfCustomersPendingLastMonth = pendingCustomers - emistatuses.NoOfCustomersPaidLastMonth;
            return Ok(emistatuses);
        }
        // PUT api/LoanAccounts/5
        public IHttpActionResult PutLoanAccount(int id, LoanAccount loanaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != loanaccount.LoanAccountId)
            {
                return BadRequest();
            }
            db.Entry(loanaccount).State = System.Data.Entity.EntityState.Modified;

            try
            {
    db.SaveChanges();
}
catch (DbUpdateConcurrencyException)
{
    if (!LoanAccountExists(id))
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
        // POST api/LoanAccounts
        [ResponseType(typeof(LoanAccount))]
public IHttpActionResult PostLoanAccount(LoanAccount loanaccount)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }
    db.LoanAccounts.Add(loanaccount);
    db.SaveChanges();
    return CreatedAtRoute("DefaultApi", new { id = loanaccount.LoanAccountId }, loanaccount);
}
// DELETE api/LoanAccounts/5
[ResponseType(typeof(LoanAccount))]
public IHttpActionResult DeleteLoanAccount(int id)
{
    LoanAccount loanaccount = db.LoanAccounts.Find(id);
    if (loanaccount == null)
    {
        return NotFound();
    }
    db.LoanAccounts.Remove(loanaccount);
    db.SaveChanges();
    return Ok(loanaccount);
}
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        db.Dispose();
    }
    base.Dispose(disposing);
}
private bool LoanAccountExists(int id)
{
    return db.LoanAccounts.Count(e => e.LoanAccountId == id) > 0;
}
    }
}