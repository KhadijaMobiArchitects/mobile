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
    public class CongesController : ControllerBase
    {
        private readonly RHDbContext _context;

        public CongesController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/Conges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conge>>> GetConges()
        {
            return await _context.Conges.ToListAsync();
        }

        // GET: api/Conges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conge>> GetConge(int id)
        {
            var conge = await _context.Conges.FindAsync(id);

            if (conge == null)
            {
                return NotFound();
            }

            return conge;
        }

        // PUT: api/Conges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConge(int id, Conge conge)
        {
            if (id != conge.Id)
            {
                return BadRequest();
            }

            _context.Entry(conge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongeExists(id))
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

        // POST: api/Conges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conge>> PostConge(Conge conge)
        {
            _context.Conges.Add(conge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConge", new { id = conge.Id }, conge);
        }

        // DELETE: api/Conges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConge(int id)
        {
            var conge = await _context.Conges.FindAsync(id);
            if (conge == null)
            {
                return NotFound();
            }

            _context.Conges.Remove(conge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CongeExists(int id)
        {
            return _context.Conges.Any(e => e.Id == id);
        }
    }
}
