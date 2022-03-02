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
    public class RefSituationProjetsController : ControllerBase
    {
        private readonly RHDbContext _context;

        public RefSituationProjetsController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/RefSituationProjets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefSituationProjet>>> GetRefSituationProjets()
        {
            return await _context.RefSituationProjets.ToListAsync();
        }

        // GET: api/RefSituationProjets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefSituationProjet>> GetRefSituationProjet(int id)
        {
            var refSituationProjet = await _context.RefSituationProjets.FindAsync(id);

            if (refSituationProjet == null)
            {
                return NotFound();
            }

            return refSituationProjet;
        }

        // PUT: api/RefSituationProjets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefSituationProjet(int id, RefSituationProjet refSituationProjet)
        {
            if (id != refSituationProjet.Id)
            {
                return BadRequest();
            }

            _context.Entry(refSituationProjet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefSituationProjetExists(id))
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

        // POST: api/RefSituationProjets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefSituationProjet>> PostRefSituationProjet(RefSituationProjet refSituationProjet)
        {
            _context.RefSituationProjets.Add(refSituationProjet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefSituationProjet", new { id = refSituationProjet.Id }, refSituationProjet);
        }

        // DELETE: api/RefSituationProjets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefSituationProjet(int id)
        {
            var refSituationProjet = await _context.RefSituationProjets.FindAsync(id);
            if (refSituationProjet == null)
            {
                return NotFound();
            }

            _context.RefSituationProjets.Remove(refSituationProjet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefSituationProjetExists(int id)
        {
            return _context.RefSituationProjets.Any(e => e.Id == id);
        }
    }
}
