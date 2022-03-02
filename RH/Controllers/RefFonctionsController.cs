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
    public class RefFonctionsController : ControllerBase
    {
        private readonly RHDbContext _context;

        public RefFonctionsController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/RefFonctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefFonction>>> GetRefFonctions()
        {
            return await _context.RefFonctions.ToListAsync();
        }

        // GET: api/RefFonctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefFonction>> GetRefFonction(int id)
        {
            var refFonction = await _context.RefFonctions.FindAsync(id);

            if (refFonction == null)
            {
                return NotFound();
            }

            return refFonction;
        }

        // PUT: api/RefFonctions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefFonction(int id, RefFonction refFonction)
        {
            if (id != refFonction.Id)
            {
                return BadRequest();
            }

            _context.Entry(refFonction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefFonctionExists(id))
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

        // POST: api/RefFonctions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefFonction>> PostRefFonction(RefFonction refFonction)
        {
            _context.RefFonctions.Add(refFonction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefFonction", new { id = refFonction.Id }, refFonction);
        }

        // DELETE: api/RefFonctions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefFonction(int id)
        {
            var refFonction = await _context.RefFonctions.FindAsync(id);
            if (refFonction == null)
            {
                return NotFound();
            }

            _context.RefFonctions.Remove(refFonction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefFonctionExists(int id)
        {
            return _context.RefFonctions.Any(e => e.Id == id);
        }
    }
}
