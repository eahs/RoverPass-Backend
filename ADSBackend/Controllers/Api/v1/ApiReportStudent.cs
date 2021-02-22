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
    public class ApiReportStudent : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiReportStudent(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiReportStudent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportStudent>>> GetReportStudent()
        {
            return await _context.ReportStudent.ToListAsync();
        }

        // GET: api/ApiReportStudent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportStudent>> GetReportStudent(int id)
        {
            var reportStudent = await _context.ReportStudent.FindAsync(id);

            if (reportStudent == null)
            {
                return NotFound();
            }

            return reportStudent;
        }

        // PUT: api/ApiReportStudent/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportStudent(int id, ReportStudent reportStudent)
        {
            if (id != reportStudent.ReportId)
            {
                return BadRequest();
            }

            _context.Entry(reportStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportStudentExists(id))
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

        // POST: api/ApiReportStudent
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReportStudent>> PostReportStudent(ReportStudent reportStudent)
        {
            _context.ReportStudent.Add(reportStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportStudent", new { id = reportStudent.ReportId }, reportStudent);
        }

        // DELETE: api/ApiReportStudent/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReportStudent>> DeleteReportStudent(int id)
        {
            var reportStudent = await _context.ReportStudent.FindAsync(id);
            if (reportStudent == null)
            {
                return NotFound();
            }

            _context.ReportStudent.Remove(reportStudent);
            await _context.SaveChangesAsync();

            return reportStudent;
        }

        private bool ReportStudentExists(int id)
        {
            return _context.ReportStudent.Any(e => e.ReportId == id);
        }
    }
}
