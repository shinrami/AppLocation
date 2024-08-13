using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations(
            [FromQuery] string name,
            [FromQuery] string address,
            [FromQuery] string coordinates)
        {
            var query = _context.Locations.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(l => l.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(l => l.Address.Contains(address));
            }

            if (!string.IsNullOrEmpty(coordinates))
            {
                query = query.Where(l => l.Coordinates.Contains(coordinates));
            }

            return await query.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
