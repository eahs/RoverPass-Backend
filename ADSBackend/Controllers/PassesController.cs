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
    public class PassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Passes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pass.Include(p => p.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Passes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pass = await _context.Pass
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.PassId == id);
            if (pass == null)
            {
                return NotFound();
            }

            return View(pass);
        }

        // GET: Passes/Create
        public IActionResult Create()
        {
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email");
            return View();
        }

        // POST: Passes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassId,Reason,IsssuedDate,memberId")] Pass pass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email", pass.memberId);
            return View(pass);
        }

        // GET: Passes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pass = await _context.Pass.FindAsync(id);
            if (pass == null)
            {
                return NotFound();
            }
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email", pass.memberId);
            return View(pass);
        }

        // POST: Passes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassId,Reason,IsssuedDate,memberId")] Pass pass)
        {
            if (id != pass.PassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassExists(pass.PassId))
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
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email", pass.memberId);
            return View(pass);
        }

        // GET: Passes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pass = await _context.Pass
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.PassId == id);
            if (pass == null)
            {
                return NotFound();
            }

            return View(pass);
        }

        // POST: Passes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pass = await _context.Pass.FindAsync(id);
            _context.Pass.Remove(pass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassExists(int id)
        {
            return _context.Pass.Any(e => e.PassId == id);
        }
    }
}
