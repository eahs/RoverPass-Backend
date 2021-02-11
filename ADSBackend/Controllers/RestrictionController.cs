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
    public class RestrictionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestrictionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Restriction
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Restriction.Include(r => r.cName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Restriction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restriction = await _context.Restriction
                .Include(r => r.cName)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (restriction == null)
            {
                return NotFound();
            }

            return View(restriction);
        }

        // GET: Restriction/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "JoinCode");
            return View();
        }

        // POST: Restriction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,IssuedDate,ClassId,restrictionType")] Restriction restriction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restriction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "JoinCode", restriction.ClassId);
            return View(restriction);
        }

        // GET: Restriction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restriction = await _context.Restriction.FindAsync(id);
            if (restriction == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "JoinCode", restriction.ClassId);
            return View(restriction);
        }

        // POST: Restriction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,IssuedDate,ClassId,restrictionType")] Restriction restriction)
        {
            if (id != restriction.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restriction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestrictionExists(restriction.UserId))
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
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "JoinCode", restriction.ClassId);
            return View(restriction);
        }

        // GET: Restriction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restriction = await _context.Restriction
                .Include(r => r.cName)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (restriction == null)
            {
                return NotFound();
            }

            return View(restriction);
        }

        // POST: Restriction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restriction = await _context.Restriction.FindAsync(id);
            _context.Restriction.Remove(restriction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestrictionExists(int id)
        {
            return _context.Restriction.Any(e => e.UserId == id);
        }
    }
}
