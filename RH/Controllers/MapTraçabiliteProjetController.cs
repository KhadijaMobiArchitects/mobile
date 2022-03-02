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
    public class MapTraçabiliteProjetController : ControllerBase
    {
        private readonly RHDbContext _context;

        public MapTraçabiliteProjetController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/MapTraçabiliteProjet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapTraçabiliteProjet>>> GetMapTraçabiliteProjets()
        {
            return await _context.MapTraçabiliteProjets.ToListAsync();
        }

        // GET: api/MapTraçabiliteProjet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MapTraçabiliteProjet>> GetMapTraçabiliteProjet(int id)
        {
            var mapTraçabiliteProjet = await _context.MapTraçabiliteProjets.FindAsync(id);

            if (mapTraçabiliteProjet == null)
            {
                return NotFound();
            }

            return mapTraçabiliteProjet;
        }

        // PUT: api/MapTraçabiliteProjet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapTraçabiliteProjet(int id, MapTraçabiliteProjet mapTraçabiliteProjet)
        {
            if (id != mapTraçabiliteProjet.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapTraçabiliteProjet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapTraçabiliteProjetExists(id))
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

        // POST: api/MapTraçabiliteProjet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MapTraçabiliteProjet>> PostMapTraçabiliteProjet(MapTraçabiliteProjet mapTraçabiliteProjet)
        {
            _context.MapTraçabiliteProjets.Add(mapTraçabiliteProjet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMapTraçabiliteProjet", new { id = mapTraçabiliteProjet.Id }, mapTraçabiliteProjet);
        }

        // DELETE: api/MapTraçabiliteProjet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapTraçabiliteProjet(int id)
        {
            var mapTraçabiliteProjet = await _context.MapTraçabiliteProjets.FindAsync(id);
            if (mapTraçabiliteProjet == null)
            {
                return NotFound();
            }

            _context.MapTraçabiliteProjets.Remove(mapTraçabiliteProjet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MapTraçabiliteProjetExists(int id)
        {
            return _context.MapTraçabiliteProjets.Any(e => e.Id == id);
        }
    }
}
