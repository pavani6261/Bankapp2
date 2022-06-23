﻿using System;
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
    public class StatesController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/States
        public IQueryable<State> GetStates()
        {
            return db.States;
        }
        // GET api/States/5
        [ResponseType(typeof(State))]
        public IHttpActionResult GetState(int id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
        // PUT api/States/5
        public IHttpActionResult PutState(int id, State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != state.StateId)
            {
                return BadRequest();
            }
            db.Entry(state).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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
        // POST api/States
        [ResponseType(typeof(State))]
        public IHttpActionResult PostState(State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.States.Add(state);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = state.StateId }, state);
        }
        // DELETE api/States/5
        [ResponseType(typeof(State))]
        public IHttpActionResult DeleteState(int id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }
            db.States.Remove(state);
            db.SaveChanges();
            return Ok(state);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool StateExists(int id)
        {
            return db.States.Count(e => e.StateId == id) > 0;
        }
    }
}