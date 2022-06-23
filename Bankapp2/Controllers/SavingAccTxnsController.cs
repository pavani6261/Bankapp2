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
    public class SavingAccTxnsController : ApiController
    {
        private TestBankDbContext db = new TestBankDbContext();
        // GET api/SavingAccTxns
        public IQueryable<SavingAccTxn> GetSavingAccTxns()
        {
            return db.SavingAccTxns;
        }
        //custom method
        // GET api/SavingAccTxns
        //[ActionName("GetSavingAccTxnsByAccountId")]
        //[HttpGet]
        //public List<SavingAccTxn> GetSavingAccTxnsByAccountId(int id)
        //{
        //    List<SavingAccTxn> savingAccList= new List<SavingAccTxn>();
        //    foreach (SavingAccTxn savingAcc in db.SavingAccTxns)
        //    {
        //        if (savingAcc.SavingAccountId == id)
        //        {
        //            savingAccList.Add(savingAcc);
        //        }
        //    }
        //    return savingAccList;
        //}
        public List<SavingAccTxn> GetSavingAccTxnsByAccountId(int id)
        {
            //new method implementation
            List<SavingAccTxn> savingAccTxnsList = new List<SavingAccTxn>();
            foreach (SavingAccount savingAccount in db.SavingAccounts.ToList())
            {
                if (savingAccount.CustAccountId == id)
                {
                    foreach (SavingAccTxn savingtxn in db.SavingAccTxns.ToList())
                    {
                        if (savingAccount.SavingAccountId == savingtxn.SavingAccountId)
                        {
                            savingAccTxnsList.Add(savingtxn);
                        }
                    }
                    break;
                }
            }
            return savingAccTxnsList;
        }
        // GET api/SavingAccTxns/5
        [ResponseType(typeof(SavingAccTxn))]
        public IHttpActionResult GetSavingAccTxn(int id)
        {
            SavingAccTxn savingacctxn = db.SavingAccTxns.Find(id);
            if (savingacctxn == null)
            {
                return NotFound();
            }
            return Ok(savingacctxn);
        }
        // PUT api/SavingAccTxns/5
        public IHttpActionResult PutSavingAccTxn(int id, SavingAccTxn savingacctxn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != savingacctxn.SavingAccTxnId)
            {
                return BadRequest();
            }
            db.Entry(savingacctxn).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavingAccTxnExists(id))
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
        //POST api/SavingAccTxns
        //[ResponseType(typeof(SavingAccTxn))]
        //public IHttpActionResult PostSavingAccTxn(SavingAccTxn savingacctxn)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    db.SavingAccTxns.Add(savingacctxn);
        //    db.SaveChanges();
        //    return CreatedAtRoute("DefaultApi", new { id = savingacctxn.SavingAccTxnId }, savingacctxn);
        //}
        [HttpPost]
        [ResponseType(typeof(SavingAccTxn))]
        [Route("api/SavingAccTxns/Deposit")]
        public IHttpActionResult Deposit(SavingAccTxn savingacctxn)
        {
            //return bad request if savingaccountId not found in db.savingaccounts
            bool isAccountValid = false;
            if (savingacctxn == null)
            {
                return BadRequest(ModelState);
            }
            if (savingacctxn.Amount <= 0)
            {
                return BadRequest("Enter a valid amount");
            }
            SavingAccount savingAccount = new SavingAccount();
            foreach (SavingAccount sa in db.SavingAccounts.ToList())
            {
                if (sa.SavingAccountId == savingacctxn.SavingAccountId)
                {
                    savingAccount = sa;
                    isAccountValid = true;
                    break;
                }
            }
            if (isAccountValid && (savingacctxn.Amount > 0))
            {
                savingAccount.Balance = savingAccount.Balance + savingacctxn.Amount;
                db.Entry(savingAccount).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //do nothing
                    return BadRequest("Backend Exception ");
                }
                savingacctxn.Balance = savingAccount.Balance;
                savingacctxn.TxnDate = DateTime.Now;
                savingacctxn.TxnDetails = " +" + savingacctxn.Amount;
                db.SavingAccTxns.Add(savingacctxn);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { controller = "SavingAccTxns", action = "Deposit", id = savingacctxn.SavingAccTxnId }, savingacctxn);
                //new { controller = "Users", action = "Manage", id = aggregateId });
            }
            else
            {
                return BadRequest("Unknown error");
            }
        }
        [HttpPost]
        [ResponseType(typeof(SavingAccTxn))]
        [Route("api/SavingAccTxns/Withdraw")]
        public IHttpActionResult Withdraw(SavingAccTxn savingacctxn)
        {
            //return bad request if savingaccountId not found in db.savingaccounts
            bool isAccountValid = false;
            if (savingacctxn == null)
            {
                return BadRequest(ModelState);
            }
            if (savingacctxn.Amount <= 0)
            {
                return BadRequest("Enter a valid amount");
            }
            SavingAccount savingAccount = new SavingAccount();
            foreach (SavingAccount sa in db.SavingAccounts.ToList())
            {
                if (sa.SavingAccountId == savingacctxn.SavingAccountId)
                {
                    savingAccount = sa;
                    isAccountValid = true;
                    break;
                }
            }
            if (isAccountValid && (savingacctxn.Amount <= savingAccount.Balance))
            {
                savingAccount.Balance = savingAccount.Balance - savingacctxn.Amount;
                db.Entry(savingAccount).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //do nothing
                    return BadRequest("Backend Exception ");
                }
                savingacctxn.Balance = savingAccount.Balance;
                savingacctxn.TxnDate = DateTime.Now;
                savingacctxn.TxnDetails = " -" + savingacctxn.Amount;
                db.SavingAccTxns.Add(savingacctxn);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { controller = "SavingAccTxns", action = "Withdraw", id = savingacctxn.SavingAccTxnId }, savingacctxn);
            }
            else
            {
                return BadRequest("Insufficient Balance");
            }
        }
        [HttpPost]
        [ResponseType(typeof(SavingAccTxn))]
        [Route("api/SavingAccTxns/Transfer")]
        public IHttpActionResult Transfer(TransferAmount transferamount)
        {
            if (transferamount == null)
            {
                return BadRequest(ModelState);
            }
            if (transferamount.Amount <= 0)
            {
                return BadRequest("Enter a valid amount");
            }
            SavingAccount senderSavingAccount = db.SavingAccounts.Find(transferamount.SenderSavingAccountId);
            if (senderSavingAccount == null)
            {
                return BadRequest("Sender Not Found");
            }
            if (transferamount.Amount > senderSavingAccount.TransferLimit || transferamount.Amount > senderSavingAccount.Balance)
            {
                return BadRequest("Check tranfer limit/Account balance");
            }
            SavingAccount recieverSavingAccount = new SavingAccount();
            bool isRecieverValid = false;
            foreach (SavingAccount sa in db.SavingAccounts.ToList())
            {
                if (sa.CustAccountId == transferamount.RecieverSavingAccountId)
                {
                    recieverSavingAccount = sa;
                    isRecieverValid = true;
                    break;
                }
            }
            if (!isRecieverValid)
            {
                return BadRequest("Reciever Not Found");
            }
            SavingAccTxn sendertxn = new SavingAccTxn();
            sendertxn.Remarks = transferamount.Remarks;
            sendertxn.Amount = transferamount.Amount;
            sendertxn.SavingAccountId = transferamount.SenderSavingAccountId;
            Withdraw(sendertxn);
            SavingAccTxn recievertxn = new SavingAccTxn();
            recievertxn.Remarks = transferamount.Remarks;
            recievertxn.Amount = transferamount.Amount;
            recievertxn.SavingAccountId = recieverSavingAccount.SavingAccountId;
            Deposit(recievertxn);
            return Ok();
        }
        // DELETE api/SavingAccTxns/5
        [ResponseType(typeof(SavingAccTxn))]
        public IHttpActionResult DeleteSavingAccTxn(int id)
        {
            SavingAccTxn savingacctxn = db.SavingAccTxns.Find(id);
            if (savingacctxn == null)
            {
                return NotFound();
            }
            db.SavingAccTxns.Remove(savingacctxn);
            db.SaveChanges();
            return Ok(savingacctxn);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SavingAccTxnExists(int id)
        {
            return db.SavingAccTxns.Count(e => e.SavingAccTxnId == id) > 0;
        }
    }
}