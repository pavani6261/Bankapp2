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
    public class SavingAccountsController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/SavingAccounts
        public IQueryable<SavingAccount> GetSavingAccounts()
        {
            return db.SavingAccounts;
        }
        // GET api/SavingAccounts/5
        [ResponseType(typeof(SavingAccount))]
        public IHttpActionResult GetSavingAccount(int id)
        {
            //SavingAccount savingaccount = db.SavingAccounts.Find(id);
            //if (savingaccount == null)
            //{
            //    return NotFound();
            //}
            //return Ok(savingaccount);
            foreach (SavingAccount sa in db.SavingAccounts.ToList())
            {
                if (sa.CustAccountId == id)
                {
                    return Ok(sa);
                }
            }
            return NotFound();
        }
        // PUT api/SavingAccounts/5
        public IHttpActionResult PutSavingAccount(int id, SavingAccount savingaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != savingaccount.SavingAccountId)
            {
                return BadRequest();
            }
            db.Entry(savingaccount).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavingAccountExists(id))
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
        // POST api/SavingAccounts
        [ResponseType(typeof(SavingAccount))]
        public IHttpActionResult PostSavingAccount(SavingAccount savingaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.SavingAccounts.Add(savingaccount);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = savingaccount.SavingAccountId }, savingaccount);
        }
        // DELETE api/SavingAccounts/5
        [ResponseType(typeof(SavingAccount))]
        public IHttpActionResult DeleteSavingAccount(int id)
        {
            SavingAccount savingaccount = db.SavingAccounts.Find(id);
            if (savingaccount == null)
            {
                return NotFound();
            }
            db.SavingAccounts.Remove(savingaccount);
            db.SaveChanges();
            return Ok(savingaccount);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool SavingAccountExists(int id)
        {
            return db.SavingAccounts.Count(e => e.SavingAccountId == id) > 0;
        }
    }
}