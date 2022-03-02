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
    public class MapTraçabiliteCongeController : ControllerBase
    {
        private readonly RHDbContext _context;

        public MapTraçabiliteCongeController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/MapTraçabiliteConge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapTraçabiliteConge>>> GetMapTraçabiliteConges()
        {
            return await _context.MapTraçabiliteConges.ToListAsync();
        }

        // GET: api/MapTraçabiliteConge/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MapTraçabiliteConge>> GetMapTraçabiliteConge(int id)
        {
            var mapTraçabiliteConge = await _context.MapTraçabiliteConges.FindAsync(id);

            if (mapTraçabiliteConge == null)
            {
                return NotFound();
            }

            return mapTraçabiliteConge;
        }

        // PUT: api/MapTraçabiliteConge/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapTraçabiliteConge(int id, MapTraçabiliteConge mapTraçabiliteConge)
        {
            if (id != mapTraçabiliteConge.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapTraçabiliteConge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapTraçabiliteCongeExists(id))
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

        // POST: api/MapTraçabiliteConge
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MapTraçabiliteConge>> PostMapTraçabiliteConge(MapTraçabiliteConge mapTraçabiliteConge)
        {
            _context.MapTraçabiliteConges.Add(mapTraçabiliteConge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMapTraçabiliteConge", new { id = mapTraçabiliteConge.Id }, mapTraçabiliteConge);
        }

        // DELETE: api/MapTraçabiliteConge/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapTraçabiliteConge(int id)
        {
            var mapTraçabiliteConge = await _context.MapTraçabiliteConges.FindAsync(id);
            if (mapTraçabiliteConge == null)
            {
                return NotFound();
            }

            _context.MapTraçabiliteConges.Remove(mapTraçabiliteConge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MapTraçabiliteCongeExists(int id)
        {
            return _context.MapTraçabiliteConges.Any(e => e.Id == id);
        }
    }
}
