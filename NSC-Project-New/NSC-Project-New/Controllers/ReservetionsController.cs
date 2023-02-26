using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSC_project.Data;
using NSC_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSC_project.Controllers
{
    public class ReservetionsController : Controller
    {
        private readonly NSC_projectContext _context;

        public ReservetionsController(NSC_projectContext context)
        {
            _context = context;
        }

        // GET: Reservetions
        public async Task<IActionResult> Index()
        {
            var nSC_projectContext = _context.Reservetion.Include(r => r.Screening).Include(r => r.User);
            return View(await nSC_projectContext.ToListAsync());
        }

        // GET: Reservetions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservetion == null)
            {
                return NotFound();
            }

            var reservetion = await _context.Reservetion
                .Include(r => r.Screening)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservetion == null)
            {
                return NotFound();
            }

            return View(reservetion);
        }

        // GET: Reservetions/Create
        public IActionResult Create()
        {
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return View();
        }

        // POST: Reservetions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,UserId,ScreeningId")] Reservetion reservetion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservetion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id", reservetion.ScreeningId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", reservetion.UserId);
            return View(reservetion);
        }

        // GET: Reservetions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservetion == null)
            {
                return NotFound();
            }

            var reservetion = await _context.Reservetion.FindAsync(id);
            if (reservetion == null)
            {
                return NotFound();
            }
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id", reservetion.ScreeningId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", reservetion.UserId);
            return View(reservetion);
        }

        // POST: Reservetions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,UserId,ScreeningId")] Reservetion reservetion)
        {
            if (id != reservetion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservetion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservetionExists(reservetion.Id))
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
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id", reservetion.ScreeningId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", reservetion.UserId);
            return View(reservetion);
        }

        // GET: Reservetions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservetion == null)
            {
                return NotFound();
            }

            var reservetion = await _context.Reservetion
                .Include(r => r.Screening)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservetion == null)
            {
                return NotFound();
            }

            return View(reservetion);
        }

        // POST: Reservetions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservetion == null)
            {
                return Problem("Entity set 'NSC_projectContext.Reservetion'  is null.");
            }
            var reservetion = await _context.Reservetion.FindAsync(id);
            if (reservetion != null)
            {
                _context.Reservetion.Remove(reservetion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservetionExists(int id)
        {
            return (_context.Reservetion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
