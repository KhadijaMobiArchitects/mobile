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
    public class RefTypeCongesController : ControllerBase
    {
        private readonly RHDbContext _context;

        public RefTypeCongesController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/RefTypeConges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefTypeConge>>> GetRefTypeConges()
        {
            return await _context.RefTypeConges.ToListAsync();
        }

        // GET: api/RefTypeConges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefTypeConge>> GetRefTypeConge(int id)
        {
            var refTypeConge = await _context.RefTypeConges.FindAsync(id);

            if (refTypeConge == null)
            {
                return NotFound();
            }

            return refTypeConge;
        }

        // PUT: api/RefTypeConges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefTypeConge(int id, RefTypeConge refTypeConge)
        {
            if (id != refTypeConge.Id)
            {
                return BadRequest();
            }

            _context.Entry(refTypeConge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefTypeCongeExists(id))
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

        // POST: api/RefTypeConges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefTypeConge>> PostRefTypeConge(RefTypeConge refTypeConge)
        {
            _context.RefTypeConges.Add(refTypeConge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefTypeConge", new { id = refTypeConge.Id }, refTypeConge);
        }

        // DELETE: api/RefTypeConges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefTypeConge(int id)
        {
            var refTypeConge = await _context.RefTypeConges.FindAsync(id);
            if (refTypeConge == null)
            {
                return NotFound();
            }

            _context.RefTypeConges.Remove(refTypeConge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefTypeCongeExists(int id)
        {
            return _context.RefTypeConges.Any(e => e.Id == id);
        }
    }
}
