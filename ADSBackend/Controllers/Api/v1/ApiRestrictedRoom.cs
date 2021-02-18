using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADSBackend.Data;
using ADSBackend.Models;

namespace ADSBackend.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiRestrictedRoom : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiRestrictedRoom(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiRestrictedRoom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestrictedRoom>>> GetRestrictedRoom()
        {
            return await _context.RestrictedRoom.ToListAsync();
        }

        // GET: api/ApiRestrictedRoom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestrictedRoom>> GetRestrictedRoom(int id)
        {
            var restrictedRoom = await _context.RestrictedRoom.FindAsync(id);

            if (restrictedRoom == null)
            {
                return NotFound();
            }

            return restrictedRoom;
        }

        // PUT: api/ApiRestrictedRoom/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestrictedRoom(int id, RestrictedRoom restrictedRoom)
        {
            if (id != restrictedRoom.RestrictedRoomId)
            {
                return BadRequest();
            }

            _context.Entry(restrictedRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestrictedRoomExists(id))
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

        // POST: api/ApiRestrictedRoom
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RestrictedRoom>> PostRestrictedRoom(RestrictedRoom restrictedRoom)
        {
            _context.RestrictedRoom.Add(restrictedRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestrictedRoom", new { id = restrictedRoom.RestrictedRoomId }, restrictedRoom);
        }

        // DELETE: api/ApiRestrictedRoom/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RestrictedRoom>> DeleteRestrictedRoom(int id)
        {
            var restrictedRoom = await _context.RestrictedRoom.FindAsync(id);
            if (restrictedRoom == null)
            {
                return NotFound();
            }

            _context.RestrictedRoom.Remove(restrictedRoom);
            await _context.SaveChangesAsync();

            return restrictedRoom;
        }

        private bool RestrictedRoomExists(int id)
        {
            return _context.RestrictedRoom.Any(e => e.RestrictedRoomId == id);
        }
    }
}
