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
    public class ZipCodesController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/ZipCodes
        public IQueryable<ZipCode> GetZipCodes()
        {
            return db.ZipCodes;
        }
        // GET api/ZipCodes/5
        [ResponseType(typeof(ZipCode))]
        public IHttpActionResult GetZipCode(int id)
        {
            ZipCode zipcode = db.ZipCodes.Find(id);
            if (zipcode == null)
            {
                return NotFound();
            }
            return Ok(zipcode);
        }
        // PUT api/ZipCodes/5
        public IHttpActionResult PutZipCode(int id, ZipCode zipcode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != zipcode.ZipCodeId)
            {
                return BadRequest();
            }
            db.Entry(zipcode).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZipCodeExists(id))
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
        // POST api/ZipCodes
        [ResponseType(typeof(ZipCode))]
        public IHttpActionResult PostZipCode(ZipCode zipcode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.ZipCodes.Add(zipcode);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = zipcode.ZipCodeId }, zipcode);
        }
        // DELETE api/ZipCodes/5
        [ResponseType(typeof(ZipCode))]
        public IHttpActionResult DeleteZipCode(int id)
        {
            ZipCode zipcode = db.ZipCodes.Find(id);
            if (zipcode == null)
            {
                return NotFound();
            }
            db.ZipCodes.Remove(zipcode);
            db.SaveChanges();
            return Ok(zipcode);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ZipCodeExists(int id)
        {
            return db.ZipCodes.Count(e => e.ZipCodeId == id) > 0;
        }
    }
}