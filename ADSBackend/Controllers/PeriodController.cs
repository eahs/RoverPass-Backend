using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADSBackend.Data;
using ADSBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace ADSBackend.Controllers
{
    [Authorize(Roles="Admin")]
    public class PeriodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeriodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Period
        public async Task<IActionResult> Index()
        {
            return View(await _context.Period.ToListAsync());
        }

        // GET: Period/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.PeriodId == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // GET: Period/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Period/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeriodId,Name,Order")] Period period)
        {
            if (ModelState.IsValid)
            {
                _context.Add(period);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(period);
        }

        // GET: Period/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Period.FindAsync(id);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Period/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeriodId,Name,Order")] Period period)
        {
            if (id != period.PeriodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(period);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodExists(period.PeriodId))
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
            return View(period);
        }

        // GET: Period/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.PeriodId == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Period/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var period = await _context.Period.FindAsync(id);
            _context.Period.Remove(period);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodExists(int id)
        {
            return _context.Period.Any(e => e.PeriodId == id);
        }
    }
}
