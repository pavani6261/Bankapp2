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
    public class AccTypesController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/AccountTypes
        public IQueryable<AccountType> GetAccountTypes()
        {
            return db.AccountTypes;
        }
        // GET api/AccountTypes/5
        [ResponseType(typeof(AccountType))]
        public IHttpActionResult GetAccountType(int id)
        {
            AccountType accounttype = db.AccountTypes.Find(id);
            if (accounttype == null)
            {
                return NotFound();
            }
            return Ok(accounttype);
        }
        // PUT api/AccountTypes/5
        public IHttpActionResult PutAccountType(int id, AccountType accounttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != accounttype.AccountTypeId)
            {
                return BadRequest();
            }
            db.Entry(accounttype).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountTypeExists(id))
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
        // POST api/AccountTypes
        [ResponseType(typeof(AccountType))]
        public IHttpActionResult PostAccountType(AccountType accounttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.AccountTypes.Add(accounttype);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = accounttype.AccountTypeId }, accounttype);
        }
        // DELETE api/AccountTypes/5
        [ResponseType(typeof(AccountType))]
        public IHttpActionResult DeleteAccountType(int id)
        {
            AccountType accounttype = db.AccountTypes.Find(id);
            if (accounttype == null)
            {
                return NotFound();
            }
            db.AccountTypes.Remove(accounttype);
            db.SaveChanges();
            return Ok(accounttype);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool AccountTypeExists(int id)
        {
            return db.AccountTypes.Count(e => e.AccountTypeId == id) > 0;
        }
    }
}