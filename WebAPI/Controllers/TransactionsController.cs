using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DbEntities.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly postgresContext _context;

        public TransactionsController(postgresContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transaction.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
            var transaction = await _context.Transaction.FindAsync(id);

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

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            if (!transaction.IsValid())
            {
                return BadRequest();
            }

            transaction.Id = 0;

            try
            {
                _context.Transaction.Add(transaction);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "POST: api/Transactions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch(Exception ex)
            {
                Log.Error(ex, $"POST: api/Transactions {transaction}");
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
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "POST: api/Transactions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"POST: api/Transactions");
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
    }
}
