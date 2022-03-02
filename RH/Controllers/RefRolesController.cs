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
    public class RefRolesController : ControllerBase
    {
        private readonly RHDbContext _context;

        public RefRolesController(RHDbContext context)
        {
            _context = context;
        }

        // GET: api/RefRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefRole>>> GetRefRoles()
        {
            return await _context.RefRoles.ToListAsync();
        }

        // GET: api/RefRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RefRole>> GetRefRole(int id)
        {
            var refRole = await _context.RefRoles.FindAsync(id);

            if (refRole == null)
            {
                return NotFound();
            }

            return refRole;
        }

        // PUT: api/RefRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefRole(int id, RefRole refRole)
        {
            if (id != refRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(refRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefRoleExists(id))
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

        // POST: api/RefRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RefRole>> PostRefRole(RefRole refRole)
        {
            _context.RefRoles.Add(refRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefRole", new { id = refRole.Id }, refRole);
        }

        // DELETE: api/RefRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefRole(int id)
        {
            var refRole = await _context.RefRoles.FindAsync(id);
            if (refRole == null)
            {
                return NotFound();
            }

            _context.RefRoles.Remove(refRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefRoleExists(int id)
        {
            return _context.RefRoles.Any(e => e.Id == id);
        }
    }
}
