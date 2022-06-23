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
    public class MaritalStatusesController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/MaritalStatuses
        public IQueryable<MaritalStatus> GetMaritalStatuses()
        {
            return db.MaritalStatuses;
        }
        // GET api/MaritalStatuses/5
        [ResponseType(typeof(MaritalStatus))]
        public IHttpActionResult GetMaritalStatus(string id)
        {
            MaritalStatus maritalstatus = db.MaritalStatuses.Find(id);
            if (maritalstatus == null)
            {
                return NotFound();
            }
            return Ok(maritalstatus);
        }
        // PUT api/MaritalStatuses/5
        public IHttpActionResult PutMaritalStatus(string id, MaritalStatus maritalstatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != maritalstatus.MaritalStatusId)
            {
                return BadRequest();
            }
            db.Entry(maritalstatus).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaritalStatusExists(id))
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
        // POST api/MaritalStatuses
        [ResponseType(typeof(MaritalStatus))]
        public IHttpActionResult PostMaritalStatus(MaritalStatus maritalstatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.MaritalStatuses.Add(maritalstatus);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MaritalStatusExists(maritalstatus.MaritalStatusId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = maritalstatus.MaritalStatusId }, maritalstatus);
        }
        // DELETE api/MaritalStatuses/5
        [ResponseType(typeof(MaritalStatus))]
        public IHttpActionResult DeleteMaritalStatus(string id)
        {
            MaritalStatus maritalstatus = db.MaritalStatuses.Find(id);
            if (maritalstatus == null)
            {
                return NotFound();
            }
            db.MaritalStatuses.Remove(maritalstatus);
            db.SaveChanges();
            return Ok(maritalstatus);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool MaritalStatusExists(string id)
        {
            return db.MaritalStatuses.Count(e => e.MaritalStatusId == id) > 0;
        }
    }
}