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
    public class RestrictedRoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestrictedRoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestrictedRoom
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RestrictedRoom.Include(r => r.ClassName).Include(r => r.RoomNumber).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RestrictedRoom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restrictedRoom = await _context.RestrictedRoom
                .Include(r => r.ClassName)
                .Include(r => r.RoomNumber)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RestrictedRoomId == id);
            if (restrictedRoom == null)
            {
                return NotFound();
            }

            return View(restrictedRoom);
        }

        // GET: RestrictedRoom/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName");
            ViewData["RoomNumberId"] = new SelectList(_context.Class, "ClassId", "RoomNumber");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return View();
        }

        // POST: RestrictedRoom/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestrictedRoomId,UserId,IssuedDate,ClassId,RoomNumberId,RestrictionType")] RestrictedRoom restrictedRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restrictedRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName", restrictedRoom.ClassId);
            ViewData["RoomNumberId"] = new SelectList(_context.Class, "ClassId", "RoomNumber", restrictedRoom.RoomNumberId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", restrictedRoom.UserId);
            return View(restrictedRoom);
        }

        // GET: RestrictedRoom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restrictedRoom = await _context.RestrictedRoom.FindAsync(id);
            if (restrictedRoom == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName", restrictedRoom.ClassId);
            ViewData["RoomNumberId"] = new SelectList(_context.Class, "ClassId", "RoomNumber", restrictedRoom.RoomNumberId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", restrictedRoom.UserId);
            return View(restrictedRoom);
        }

        // POST: RestrictedRoom/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RestrictedRoomId,UserId,IssuedDate,ClassId,RoomNumberId,RestrictionType")] RestrictedRoom restrictedRoom)
        {
            if (id != restrictedRoom.RestrictedRoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restrictedRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestrictedRoomExists(restrictedRoom.RestrictedRoomId))
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
            ViewData["ClassId"] = new SelectList(_context.Class, "ClassId", "ClassName", restrictedRoom.ClassId);
            ViewData["RoomNumberId"] = new SelectList(_context.Class, "ClassId", "RoomNumber", restrictedRoom.RoomNumberId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", restrictedRoom.UserId);
            return View(restrictedRoom);
        }

        // GET: RestrictedRoom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restrictedRoom = await _context.RestrictedRoom
                .Include(r => r.ClassName)
                .Include(r => r.RoomNumber)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RestrictedRoomId == id);
            if (restrictedRoom == null)
            {
                return NotFound();
            }

            return View(restrictedRoom);
        }

        // POST: RestrictedRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restrictedRoom = await _context.RestrictedRoom.FindAsync(id);
            _context.RestrictedRoom.Remove(restrictedRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestrictedRoomExists(int id)
        {
            return _context.RestrictedRoom.Any(e => e.RestrictedRoomId == id);
        }
    }
}
