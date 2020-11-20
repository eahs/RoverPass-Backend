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
    public class PassRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PassRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PassRecord.Include(p => p.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PassRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passRecord = await _context.PassRecord
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.PassId == id);
            if (passRecord == null)
            {
                return NotFound();
            }

            return View(passRecord);
        }

        // GET: PassRecords/Create
        public IActionResult Create()
        {
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email");
            return View();
        }

        // POST: PassRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassId,Reason,IsssuedDate,memberId")] PassRecord passRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email", passRecord.memberId);
            return View(passRecord);
        }

        // GET: PassRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passRecord = await _context.PassRecord.FindAsync(id);
            if (passRecord == null)
            {
                return NotFound();
            }
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email", passRecord.memberId);
            return View(passRecord);
        }

        // POST: PassRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassId,Reason,IsssuedDate,memberId")] PassRecord passRecord)
        {
            if (id != passRecord.PassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassRecordExists(passRecord.PassId))
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
            ViewData["memberId"] = new SelectList(_context.Member, "MemberId", "Email", passRecord.memberId);
            return View(passRecord);
        }

        // GET: PassRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passRecord = await _context.PassRecord
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.PassId == id);
            if (passRecord == null)
            {
                return NotFound();
            }

            return View(passRecord);
        }

        // POST: PassRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passRecord = await _context.PassRecord.FindAsync(id);
            _context.PassRecord.Remove(passRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassRecordExists(int id)
        {
            return _context.PassRecord.Any(e => e.PassId == id);
        }
    }
}
