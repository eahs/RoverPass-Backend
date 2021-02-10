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
    public class ApiPass : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiPass(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiPass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pass>>> GetPass()
        {
            return await _context.Pass.ToListAsync();
        }

        // GET: api/ApiPass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pass>> GetPass(int id)
        {
            var pass = await _context.Pass.FindAsync(id);

            if (pass == null)
            {
                return NotFound();
            }

            return pass;
        }

        // PUT: api/ApiPass/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPass(int id, Pass pass)
        {
            if (id != pass.PassId)
            {
                return BadRequest();
            }

            _context.Entry(pass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassExists(id))
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

        // POST: api/ApiPass
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pass>> PostPass(Pass pass)
        {
            _context.Pass.Add(pass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPass", new { id = pass.PassId }, pass);
        }

        // DELETE: api/ApiPass/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pass>> DeletePass(int id)
        {
            var pass = await _context.Pass.FindAsync(id);
            if (pass == null)
            {
                return NotFound();
            }

            _context.Pass.Remove(pass);
            await _context.SaveChangesAsync();

            return pass;
        }

        private bool PassExists(int id)
        {
            return _context.Pass.Any(e => e.PassId == id);
        }
    }
}
