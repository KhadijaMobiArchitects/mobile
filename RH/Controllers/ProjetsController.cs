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
    public class ProjetsController : ControllerBase
    {
        private readonly RHDbContext _context;

        public ProjetsController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/Projets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projet>>> GetProjets()
        {
            return await _context.Projets.ToListAsync();
        }

        // GET: api/Projets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projet>> GetProjet(int id)
        {
            var projet = await _context.Projets.FindAsync(id);

            if (projet == null)
            {
                return NotFound();
            }

            return projet;
        }

        // PUT: api/Projets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjet(int id, Projet projet)
        {
            if (id != projet.Id)
            {
                return BadRequest();
            }

            _context.Entry(projet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjetExists(id))
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

        // POST: api/Projets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Projet>> PostProjet(Projet projet)
        {
            _context.Projets.Add(projet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjet", new { id = projet.Id }, projet);
        }

        // DELETE: api/Projets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjet(int id)
        {
            var projet = await _context.Projets.FindAsync(id);
            if (projet == null)
            {
                return NotFound();
            }

            _context.Projets.Remove(projet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjetExists(int id)
        {
            return _context.Projets.Any(e => e.Id == id);
        }
    }
}
