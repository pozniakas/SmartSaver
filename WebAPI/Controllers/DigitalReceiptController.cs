using DbEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebAPI.Clients;
using WebAPI.DTOModels;

namespace WebAPI.Controllers
{
    [Route("api/digital-receipt")]
    [ApiController]
    public class DigitalReceiptController: ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly DigitalReceiptClient digitalReceiptClient;
        public DigitalReceiptController(DatabaseContext context)
        {
            _context = context;
            digitalReceiptClient = new DigitalReceiptClient();
        }

        // POST: api/digital-receipt
        [HttpPost]
        public async Task<Transaction> GenerateDigitalReceiptTransaction(DigitalReceiptDTO digitalReceiptDTO)
        {
            var id = await digitalReceiptClient.ValidateData(digitalReceiptDTO.Data);
            return await digitalReceiptClient.GetDigitalReceiptTransaction(id);
        }
    }
}
