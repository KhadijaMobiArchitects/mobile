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
    public class RefTypeTraçabiliteController : ControllerBase
    {
        private readonly RHDbContext _context;

        public RefTypeTraçabiliteController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/RefTypeTraçabilite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefTypeTraçabilite>>> GetRefTypeTraçabilites()
        {
            return await _context.RefTypeTraçabilites.ToListAsync();
        }

        // GET: api/RefTypeTraçabilite/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefTypeTraçabilite>> GetRefTypeTraçabilite(int id)
        {
            var refTypeTraçabilite = await _context.RefTypeTraçabilites.FindAsync(id);

            if (refTypeTraçabilite == null)
            {
                return NotFound();
            }

            return refTypeTraçabilite;
        }

        // PUT: api/RefTypeTraçabilite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefTypeTraçabilite(int id, RefTypeTraçabilite refTypeTraçabilite)
        {
            if (id != refTypeTraçabilite.Id)
            {
                return BadRequest();
            }

            _context.Entry(refTypeTraçabilite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefTypeTraçabiliteExists(id))
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

        // POST: api/RefTypeTraçabilite
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefTypeTraçabilite>> PostRefTypeTraçabilite(RefTypeTraçabilite refTypeTraçabilite)
        {
            _context.RefTypeTraçabilites.Add(refTypeTraçabilite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefTypeTraçabilite", new { id = refTypeTraçabilite.Id }, refTypeTraçabilite);
        }

        // DELETE: api/RefTypeTraçabilite/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefTypeTraçabilite(int id)
        {
            var refTypeTraçabilite = await _context.RefTypeTraçabilites.FindAsync(id);
            if (refTypeTraçabilite == null)
            {
                return NotFound();
            }

            _context.RefTypeTraçabilites.Remove(refTypeTraçabilite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefTypeTraçabiliteExists(int id)
        {
            return _context.RefTypeTraçabilites.Any(e => e.Id == id);
        }
    }
}
