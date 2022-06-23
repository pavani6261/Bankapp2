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
    public class LoanDurationsController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/LoanDurations
        public IQueryable<LoanDuration> GetLoanDurations()
        {
            return db.LoanDurations;
        }
        // GET api/LoanDurations/5
        [ResponseType(typeof(LoanDuration))]
        public IHttpActionResult GetLoanDuration(int id)
        {
            LoanDuration loanduration = db.LoanDurations.Find(id);
            if (loanduration == null)
            {
                return NotFound();
            }
            return Ok(loanduration);
        }
        // PUT api/LoanDurations/5
        public IHttpActionResult PutLoanDuration(int id, LoanDuration loanduration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != loanduration.LoanDurationId)
            {
                return BadRequest();
            }
            db.Entry(loanduration).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanDurationExists(id))
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
        // POST api/LoanDurations
        [ResponseType(typeof(LoanDuration))]
        public IHttpActionResult PostLoanDuration(LoanDuration loanduration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.LoanDurations.Add(loanduration);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = loanduration.LoanDurationId }, loanduration);
        }
        // DELETE api/LoanDurations/5
        [ResponseType(typeof(LoanDuration))]
        public IHttpActionResult DeleteLoanDuration(int id)
        {
            LoanDuration loanduration = db.LoanDurations.Find(id);
            if (loanduration == null)
            {
                return NotFound();
            }
            db.LoanDurations.Remove(loanduration);
            db.SaveChanges();
            return Ok(loanduration);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool LoanDurationExists(int id)
        {
            return db.LoanDurations.Count(e => e.LoanDurationId == id) > 0;
        }
    }
}
