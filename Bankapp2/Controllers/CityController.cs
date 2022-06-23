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
    public class CityController : ApiController
    {
        private TestBankDbContext db = new Bankapp2.Models.TestBankDbContext();
        // GET api/City
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }
        // GET api/City/5
        [ResponseType(typeof(City))]
        public IHttpActionResult GetCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
        // PUT api/City/5
        public IHttpActionResult PutCity(int id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != city.CityId)
            {
                return BadRequest();
            }
            db.Entry(city).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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
        // POST api/City
        [ResponseType(typeof(City))]
        public IHttpActionResult PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Cities.Add(city);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = city.CityId }, city);
        }
        // DELETE api/City/5
        [ResponseType(typeof(City))]
        public IHttpActionResult DeleteCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }
            db.Cities.Remove(city);
            db.SaveChanges();
            return Ok(city);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CityExists(int id)
        {
            return db.Cities.Count(e => e.CityId == id) > 0;
        }
    }
}