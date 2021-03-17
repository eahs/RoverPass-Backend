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
    public class TimerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Timer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Timer.ToListAsync());
        }

        // GET: Timer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timer = await _context.Timer
                .FirstOrDefaultAsync(m => m.TimerId == id);
            if (timer == null)
            {
                return NotFound();
            }

            return View(timer);
        }

        // GET: Timer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Timer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimerId")] Timer timer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timer);
        }

        // GET: Timer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timer = await _context.Timer.FindAsync(id);
            if (timer == null)
            {
                return NotFound();
            }
            return View(timer);
        }

        // POST: Timer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimerId")] Timer timer)
        {
            if (id != timer.TimerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimerExists(timer.TimerId))
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
            return View(timer);
        }

        // GET: Timer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timer = await _context.Timer
                .FirstOrDefaultAsync(m => m.TimerId == id);
            if (timer == null)
            {
                return NotFound();
            }

            return View(timer);
        }

        // POST: Timer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timer = await _context.Timer.FindAsync(id);
            _context.Timer.Remove(timer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimerExists(int id)
        {
            return _context.Timer.Any(e => e.TimerId == id);
        }
    }
}
