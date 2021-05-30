using DbEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.DTOModels;

namespace WebAPI.Controllers
{
    [Route("api/digital-receipt")]
    [ApiController]
    public class DigitalReceiptController: ControllerBase
    {
        private readonly DatabaseContext _context;

        public DigitalReceiptController(DatabaseContext context)
        {
            _context = context;
        }

        // POST: api/digital-receipt
        [HttpPost]
        public Transaction GenerateDigitalReceiptTransaction(DigitalReceiptDTO digitalReceiptDTO)
        {
            return new Transaction() { Amount = 15, CounterParty = "Maxima", Details = "Very nice shopping", TrTime = DateTime.Now };
        }
    }
}
