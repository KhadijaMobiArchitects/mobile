#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RH.Repository;
using RH.Repository.Models;

namespace RH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefDecisionsController : ControllerBase
    {
        private readonly RHDbContext _context;

        public RefDecisionsController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/RefDecisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefDecision>>> GetRefDecisions()
        {
            return await _context.RefDecisions.ToListAsync();
        }

        // GET: api/RefDecisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefDecision>> GetRefDecision(int id)
        {
            var refDecision = await _context.RefDecisions.FindAsync(id);

            if (refDecision == null)
            {
                return NotFound();
            }

            return refDecision;
        }

        // PUT: api/RefDecisions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefDecision(int id, RefDecision refDecision)
        {
            if (id != refDecision.Id)
            {
                return BadRequest();
            }

            _context.Entry(refDecision).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefDecisionExists(id))
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

        // POST: api/RefDecisions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefDecision>> PostRefDecision(RefDecision refDecision)
        {
            _context.RefDecisions.Add(refDecision);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefDecision", new { id = refDecision.Id }, refDecision);
        }

        // DELETE: api/RefDecisions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefDecision(int id)
        {
            var refDecision = await _context.RefDecisions.FindAsync(id);
            if (refDecision == null)
            {
                return NotFound();
            }

            _context.RefDecisions.Remove(refDecision);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefDecisionExists(int id)
        {
            return _context.RefDecisions.Any(e => e.Id == id);
        }
    }
}
