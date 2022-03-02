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
    public class RefStatutCongesController : ControllerBase
    {
        private readonly RHDbContext _context;

        public RefStatutCongesController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/RefStatutConges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefStatutConge>>> GetRefStatutConges()
        {
            return await _context.RefStatutConges.ToListAsync();
        }

        // GET: api/RefStatutConges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefStatutConge>> GetRefStatutConge(int id)
        {
            var refStatutConge = await _context.RefStatutConges.FindAsync(id);

            if (refStatutConge == null)
            {
                return NotFound();
            }

            return refStatutConge;
        }

        // PUT: api/RefStatutConges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefStatutConge(int id, RefStatutConge refStatutConge)
        {
            if (id != refStatutConge.Id)
            {
                return BadRequest();
            }

            _context.Entry(refStatutConge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefStatutCongeExists(id))
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

        // POST: api/RefStatutConges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefStatutConge>> PostRefStatutConge(RefStatutConge refStatutConge)
        {
            _context.RefStatutConges.Add(refStatutConge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefStatutConge", new { id = refStatutConge.Id }, refStatutConge);
        }

        // DELETE: api/RefStatutConges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefStatutConge(int id)
        {
            var refStatutConge = await _context.RefStatutConges.FindAsync(id);
            if (refStatutConge == null)
            {
                return NotFound();
            }

            _context.RefStatutConges.Remove(refStatutConge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefStatutCongeExists(int id)
        {
            return _context.RefStatutConges.Any(e => e.Id == id);
        }
    }
}
