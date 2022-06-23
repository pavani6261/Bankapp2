using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bankapp2.Models;
namespace Bankapp2.Controllers
{
    public class CalculateLoanController : ApiController
    {
        // GET api/calculateloan
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/calculateloan/5
        public string Get(int id)
        {
            return "value";
        }
        // POST api/calculateloan
        [HttpPost]
        public IHttpActionResult Post(CalculateLoan id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var c = new CalculateLoan(id.RateOfInterest, id.LoanAmount, id.LoanDuration);
            return CreatedAtRoute("DefaultApi", null, c);
        }
        // PUT api/calculateloan/5
        public void Put(int id, [FromBody] string value)
        {
        }
        // DELETE api/calculateloan/5
        public void Delete(int id)
        {
        }
    }
}