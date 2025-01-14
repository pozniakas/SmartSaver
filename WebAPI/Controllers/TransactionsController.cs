﻿using DbEntities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recognizer;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI.DTOModels;
using WebAPI.Handlers;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class TransactionsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;
        private readonly ReceiptRecognizer _receiptRecognizer;

        public TransactionsController(DatabaseContext context, ILogger logger, ReceiptRecognizer receiptRecognizer)
        {
            _context = context;
            _logger = logger;
            _receiptRecognizer = receiptRecognizer;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transaction
                // (... || transaction.User == null ) should be removed when user management system is fully functional
                .Where(transaction => User.GetId() == transaction.User.Id || transaction.User == null)
                .Include(transaction => transaction.Category).AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
            var transaction = await _context.Transaction//.FindAsync(id);
                .Where(transaction => transaction.Id == id)
                .Include(transaction => transaction.Category).AsNoTracking()
                .SingleOrDefaultAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(long id, Transaction transaction)
        {
            if (id != transaction.Id || !transaction.IsValid())
            {
                return BadRequest();
            }

            try
            {
                var trackedTransaction = _context.Transaction.Find(id);
                trackedTransaction.Update(transaction);
                _context.Entry(trackedTransaction).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }

            return NoContent();
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            var transactionExists = await _context.Transaction.FirstOrDefaultAsync(IsNew(transaction)) != null;

            if (!transaction.IsValid() || transactionExists)
            {
                return BadRequest();
            }

            transaction.Id = 0;

            try
            {
                if (transaction.Category != null)
                {
                    var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == transaction.Category.Id);
                    if (category == null)
                    {
                        return BadRequest();
                    }

                    transaction.Category = category;
                    //category.Transactions.Add(transaction);
                }

                _context.Transaction.Add(transaction);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"POST: api/Transactions {transaction}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("file")]
        // POST: api/Transactions/file
        public async Task<IActionResult> TransactionsFromFile([FromForm(Name = "file")] IFormFile file)
        {
            try
            {
                var streamReader = new StreamReader(file.OpenReadStream());
                var bankStatmentReader = new BankStatmentReader(streamReader);
                var transactions = bankStatmentReader.Read();

                transactions = transactions.Where(tr => tr != null);

                _context.Transaction.AddRange(transactions);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTransactions", transactions);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"POST: api/Transactions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("receiptImage")]
        // POST: api/Transactions/receiptImage
        public async Task<IActionResult> TransactionsFromReceipt([FromForm] IFormFile image)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);

                var transaction = await _receiptRecognizer.Recognize(memoryStream);

                _context.Transaction.Add(transaction);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"POST: api/Transactions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaction>> DeleteTransaction(long id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        private bool TransactionExists(long id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }

        public Expression<Func<Transaction, bool>> IsNew(Transaction transaction)
        {
            return x => x.TrTime == transaction.TrTime && x.Amount == transaction.Amount;
        }
    }
}
