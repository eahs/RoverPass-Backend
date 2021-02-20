using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADSBackend.Data;
using ADSBackend.Models;

namespace ADSBackend.Controllers
{
    public class ReportStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportStudents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReportStudent.Include(r => r.Name).Include(r => r.Student).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReportStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportStudent = await _context.ReportStudent
                .Include(r => r.Name)
                .Include(r => r.Student)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reportStudent == null)
            {
                return NotFound();
            }

            return View(reportStudent);
        }

        // GET: ReportStudents/Create
        public IActionResult Create()
        {
            ViewData["NameId"] = new SelectList(_context.ReportType, "ReportTypeId", "Name");
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: ReportStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,UserId,StudentId,NameId,Description")] ReportStudent reportStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NameId"] = new SelectList(_context.ReportType, "ReportTypeId", "Name", reportStudent.NameId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "FirstName", reportStudent.StudentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", reportStudent.UserId);
            return View(reportStudent);
        }

        // GET: ReportStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportStudent = await _context.ReportStudent.FindAsync(id);
            if (reportStudent == null)
            {
                return NotFound();
            }
            ViewData["NameId"] = new SelectList(_context.ReportType, "ReportTypeId", "Name", reportStudent.NameId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "FirstName", reportStudent.StudentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", reportStudent.UserId);
            return View(reportStudent);
        }

        // POST: ReportStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,UserId,StudentId,NameId,Description")] ReportStudent reportStudent)
        {
            if (id != reportStudent.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportStudentExists(reportStudent.ReportId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NameId"] = new SelectList(_context.ReportType, "ReportTypeId", "Name", reportStudent.NameId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "FirstName", reportStudent.StudentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", reportStudent.UserId);
            return View(reportStudent);
        }

        // GET: ReportStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportStudent = await _context.ReportStudent
                .Include(r => r.Name)
                .Include(r => r.Student)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reportStudent == null)
            {
                return NotFound();
            }

            return View(reportStudent);
        }

        // POST: ReportStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportStudent = await _context.ReportStudent.FindAsync(id);
            _context.ReportStudent.Remove(reportStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportStudentExists(int id)
        {
            return _context.ReportStudent.Any(e => e.ReportId == id);
        }
    }
}
