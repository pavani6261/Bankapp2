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
    public class CountryController : ApiController
    {
        private Bankapp2.Models.TestBankDbContext db = new Bankapp2.Models.TestBankDbContext();
        // GET api/Country
        public IQueryable<Country> GetCountries()
        {
            return db.Countries;
        }
        // GET api/Country/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        // PUT api/Country/5
        public IHttpActionResult PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != country.CountryId)
            {
                return BadRequest();
            }
            db.Entry(country).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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
        // POST api/Country
        [ResponseType(typeof(Country))]
        public IHttpActionResult PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Countries.Add(country);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = country.CountryId }, country);
        }
        // DELETE api/Country/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult DeleteCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            db.Countries.Remove(country);
            db.SaveChanges();
            return Ok(country);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CountryExists(int id)
        {
            return db.Countries.Count(e => e.CountryId == id) > 0;
        }
    }
}