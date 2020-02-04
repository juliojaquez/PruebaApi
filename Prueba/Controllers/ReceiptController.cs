using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Prueba.Models;
using PruebaAPI.Models;

namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReceiptController : ControllerBase
    {
        private readonly CoreContext _context;
        IConfiguration _configuration;

        public ReceiptController(IConfiguration configuration, CoreContext context)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost, Route("AddReceipt")]
        public ActionResult AddReceipt([FromBody] Receipt Receipt)
        {

            _context.Receipts.Add(new Receipt
            {
                Amount = Receipt.Amount,
                Comment = Receipt.Comment,
                Currency = Receipt.Currency,
                Date = Receipt.Date,
                Provider = Receipt.Provider
            });
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete, Route("DeleteReceipt")]
        public ActionResult DeleteReceipt(int Id)
        {
            var receipt = _context.Receipts.Where(x=>x.Id == Id).FirstOrDefault();
            _context.Receipts.Remove(receipt);
            _context.SaveChanges();


            return Ok();
        }

        [HttpGet, Route("GetAll")]
        public ActionResult GetAll()
        {
            var receipts = _context.Receipts.ToList();


            return Accepted(receipts);
        }

        [HttpPost, Route("Update")]
        public ActionResult Update([FromBody] Receipt Receipt)
        {
            var Rcpt = _context.Receipts.Find(Receipt.Id);
            Rcpt.Amount = Receipt.Amount;
            Rcpt.Comment = Receipt.Comment;
            Rcpt.Currency = Receipt.Currency;
            Rcpt.Date = Receipt.Date;
            Rcpt.Provider = Receipt.Provider;

            _context.Receipts.Update(Rcpt);
            _context.SaveChanges();

            return Ok();
        }


    }
}