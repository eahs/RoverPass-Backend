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
    [Authorize(Roles = "Admin")]
    public class PassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pass
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pass
                                               .Include(p => p.PassType)
                                               .Include(p => p.Reviewer)
                                               .Include(p => p.User);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pass/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pass = await _context.Pass
                .Include(p => p.Reviewer)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PassId == id);
            if (pass == null)
            {
                return NotFound();
            }

            return View(pass);
        }

        // GET: Pass/Create
        public IActionResult Create()
        {
            ViewData["ReviewerId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["PassTypeId"] = new SelectList(_context.PassType, "PassTypeId", "Name");

            return View();
        }

        // POST: Pass/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassId,Reason,IssuedDate,UserId,ReviewerId,PassTypeId")] Pass pass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReviewerId"] = new SelectList(_context.Users, "Id", "FirstName", pass.ReviewerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", pass.UserId);
            ViewData["PassTypeId"] = new SelectList(_context.PassType, "PassTypeId", "Name", pass.PassTypeId);

            return View(pass);
        }

        // GET: Pass/Edit/5
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
            ViewData["ReviewerId"] = new SelectList(_context.Users, "Id", "FirstName", pass.ReviewerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", pass.UserId);
            ViewData["PassTypeId"] = new SelectList(_context.PassType, "PassTypeId", "Name", pass.PassTypeId);
            return View(pass);
        }

        // POST: Pass/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassId,Reason,IssuedDate,UserId,ReviewerId,PassTypeId")] Pass pass)
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
            ViewData["ReviewerId"] = new SelectList(_context.Users, "Id", "FirstName", pass.ReviewerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", pass.UserId);
            ViewData["PassTypeId"] = new SelectList(_context.PassType, "PassTypeId", "Name", pass.PassTypeId);
            return View(pass);
        }

        // GET: Pass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pass = await _context.Pass
                .Include(p => p.Reviewer)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PassId == id);
            if (pass == null)
            {
                return NotFound();
            }

            return View(pass);
        }

        // POST: Pass/Delete/5
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
