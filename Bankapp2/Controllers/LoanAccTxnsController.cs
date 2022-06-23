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
    public class LoanAccTxnsController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/LoanAccTxns
        public IQueryable<LoanAccTxn> GetLoanAccTxns()
        {
            return db.LoanAccTxns;
        }
        public List<LoanAccTxn> GetLoanAccTxnsByAccountId(int id)
        {
            //new method
            List<LoanAccTxn> loanAccTxnsList = new List<LoanAccTxn>();
            foreach (LoanAccount loanAccount in db.LoanAccounts.ToList())
            {
                if (loanAccount.CustAccountId == id)
                {
                    foreach (LoanAccTxn loantxn in db.LoanAccTxns.ToList())
                    {
                        if (loanAccount.LoanAccountId == loantxn.LoanAccountId)
                        {
                            loanAccTxnsList.Add(loantxn);
                        }
                    }
                    break;
                }
            }
            return loanAccTxnsList;
        }
        // GET api/LoanAccTxns/5
        [ResponseType(typeof(LoanAccTxn))]
        public IHttpActionResult GetLoanAccTxn(int id)
        {
            LoanAccTxn loanacctxn = db.LoanAccTxns.Find(id);
            if (loanacctxn == null)
            {
                return NotFound();
            }
            return Ok(loanacctxn);
        }
        // PUT api/LoanAccTxns/5
        public IHttpActionResult PutLoanAccTxn(int id, LoanAccTxn loanacctxn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != loanacctxn.LoanAccTxnId)
            {
                return BadRequest();
            }
            db.Entry(loanacctxn).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanAccTxnExists(id))
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
        // POST api/LoanAccTxns
        //[ResponseType(typeof(LoanAccTxn))]
        //public IHttpActionResult PostLoanAccTxn(LoanAccTxn loanacctxn)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    db.LoanAccTxns.Add(loanacctxn);
        //    db.SaveChanges();
        //    return CreatedAtRoute("DefaultApi", new { id = loanacctxn.LoanAccTxnId }, loanacctxn);
        //}
        //custom post method
        [ResponseType(typeof(LoanAccTxn))]
        public IHttpActionResult PostLoanAccTxn(LoanAccTxn loanacctxn)
        {
            if (loanacctxn == null)
            {
                return BadRequest(ModelState);
            }
            LoanAccount currentUser = new LoanAccount();
            foreach (LoanAccount la in db.LoanAccounts.ToList())
            {
                if (loanacctxn.LoanAccountId == la.LoanAccountId)
                {
                    currentUser = la;
                    break;
                }
            }
            if (loanacctxn.EmiAmount != 0)
            {
                if (loanacctxn.EmiAmount >= currentUser.BalanceAmount)
                {
                    currentUser.BalanceAmount = 0;
                    loanacctxn.Status = "deactive";
                }
                else
                {
                    currentUser.BalanceAmount = currentUser.BalanceAmount - loanacctxn.EmiAmount;
                    loanacctxn.Status = "active";
                }
                db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //do nothing
                }
                loanacctxn.EmiDate = DateTime.Now;
                loanacctxn.PendingAmount = currentUser.BalanceAmount;
                db.LoanAccTxns.Add(loanacctxn);
                db.SaveChanges();
            }
            return CreatedAtRoute("DefaultApi", new { id = loanacctxn.LoanAccTxnId }, loanacctxn);
        }
        // DELETE api/LoanAccTxns/5
        [ResponseType(typeof(LoanAccTxn))]
        public IHttpActionResult DeleteLoanAccTxn(int id)
        {
            LoanAccTxn loanacctxn = db.LoanAccTxns.Find(id);
            if (loanacctxn == null)
            {
                return NotFound();
            }
            db.LoanAccTxns.Remove(loanacctxn);
            db.SaveChanges();
            return Ok(loanacctxn);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool LoanAccTxnExists(int id)
        {
            return db.LoanAccTxns.Count(e => e.LoanAccTxnId == id) > 0;
        }
    }
}