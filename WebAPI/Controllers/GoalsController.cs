using DbEntities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public GoalsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Goals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goal>>> GetGoal()
        {
            return await _context.Goal.ToListAsync();
        }

        // GET: api/Goals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goal>> GetGoal(long id)
        {
            var goal = await _context.Goal.FindAsync(id);

            if (goal == null)
            {
                return NotFound();
            }

            return goal;
        }

        // PUT: api/Goals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoal(long id, GoalDTO dtoGoal)
        {
            if (id != dtoGoal.Id || !GoalExists(id) || dtoGoal.Amount <= 0)
            {
                return BadRequest();
            }

            var goal = await _context.Goal.FindAsync(id);
            goal.Update(dtoGoal);

            _context.Entry(goal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(id))
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



        // POST: api/Goals
        [HttpPost]
        public async Task<ActionResult<Goal>> PostGoal(GoalDTO dtoGoal)
        {
            var goal = dtoGoal.ToEntity();

            try
            {
                var deadline = new DateTime(goal.Deadlinedate.Value.Ticks);
                
                if (goal.IsValid())
                {
                    return BadRequest();
                }

                _context.Goal.Add(goal);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetGoal", new { id = goal.Id }, goal);
            }
            catch (InvalidOperationException)
            {
                dtoGoal.Deadlinedate = goal.Creationdate.AddMonths(1);

                return await PostGoal(dtoGoal);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Goals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Goal>> DeleteGoal(long id)
        {
            var goal = await _context.Goal.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            _context.Goal.Remove(goal);
            await _context.SaveChangesAsync();

            return goal;
        }

        private bool GoalExists(long id)
        {
            return _context.Goal.Any(e => e.Id == id);
        }
    }
}
