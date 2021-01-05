using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADSBackend.Data;
using ADSBackend.Models;
using ADSBackend.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ADSBackend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Class
        public async Task<IActionResult> Index()
        {
            return View(await _context.Class.ToListAsync());
        }

        // GET: Class/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(p => p.Period)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Class/Create
        public IActionResult Create()
        {
            ViewData["PeriodId"] = new SelectList(_context.Period.OrderBy(p => p.Order), "PeriodId", "Name");

            return View();
        }

        // POST: Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassId,TeacherName,PeriodId,ClassName,RoomNumber")] Class @class)
        {
            // Keep generating joincodes until you find a unique one
            while (true)
            {
                @class.JoinCode = RandomString.Generate(6);

                var cls = await _context.Class.FirstOrDefaultAsync(cl => cl.JoinCode == @class.JoinCode);

                if (cls == null)
                    break;
            }

            ModelState.Clear();
            TryValidateModel(@class);

            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.Include(p => p.Period).FirstOrDefaultAsync(p => p.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }
            ViewData["PeriodId"] = new SelectList(_context.Period.OrderBy(p => p.Order), "PeriodId", "Name");

            return View(@class);
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,TeacherName,PeriodId,ClassName,RoomNumber")] Class @class)
        {
            if (id != @class.ClassId)
            {
                return NotFound();
            }

            var cls = await _context.Class.Include(p => p.Period).FirstOrDefaultAsync(c => c.ClassId == id);

            if (cls == null)
            {
                return NotFound();
            }

            // Copy over the fields from the form
            cls.TeacherName = @class.TeacherName;
            cls.RoomNumber = @class.RoomNumber;
            cls.ClassName = @class.ClassName;
            cls.PeriodId = @class.PeriodId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.ClassId))
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

            ViewData["PeriodId"] = new SelectList(_context.Period.OrderBy(p => p.Order), "PeriodId", "Name");

            return View(cls);
        }

        // GET: Class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Class.FindAsync(id);
            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.ClassId == id);
        }
    }
}
