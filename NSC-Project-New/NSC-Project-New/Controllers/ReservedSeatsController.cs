using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSC_project.Data;
using NSC_project.Models;

namespace NSC_project.Controllers
{
    public class ReservedSeatsController : Controller
    {
        private readonly NSC_projectContext _context;

        public ReservedSeatsController(NSC_projectContext context)
        {
            _context = context;
        }

        // GET: ReservedSeats
        public async Task<IActionResult> Index()
        {
            var nSC_projectContext = _context.ReservedSeat.Include(r => r.Reservetion).Include(r => r.Screening).Include(r => r.Seat);
            return View(await nSC_projectContext.ToListAsync());
        }

        // GET: ReservedSeats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReservedSeat == null)
            {
                return NotFound();
            }

            var reservedSeat = await _context.ReservedSeat
                .Include(r => r.Reservetion)
                .Include(r => r.Screening)
                .Include(r => r.Seat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservedSeat == null)
            {
                return NotFound();
            }

            return View(reservedSeat);
        }

        // GET: ReservedSeats/Create
        public IActionResult Create()
        {
            ViewData["ReservetionId"] = new SelectList(_context.Reservetion, "Id", "Status");
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id");
            ViewData["SeatId"] = new SelectList(_context.Seat, "Id", "Id");
            return View();
        }

        // POST: ReservedSeats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ScreeningId,SeatId,ReservetionId")] ReservedSeat reservedSeat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservedSeat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservetionId"] = new SelectList(_context.Reservetion, "Id", "Status", reservedSeat.ReservetionId);
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id", reservedSeat.ScreeningId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "Id", "Id", reservedSeat.SeatId);
            return View(reservedSeat);
        }

        // GET: ReservedSeats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReservedSeat == null)
            {
                return NotFound();
            }

            var reservedSeat = await _context.ReservedSeat.FindAsync(id);
            if (reservedSeat == null)
            {
                return NotFound();
            }
            ViewData["ReservetionId"] = new SelectList(_context.Reservetion, "Id", "Status", reservedSeat.ReservetionId);
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id", reservedSeat.ScreeningId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "Id", "Id", reservedSeat.SeatId);
            return View(reservedSeat);
        }

        // POST: ReservedSeats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ScreeningId,SeatId,ReservetionId")] ReservedSeat reservedSeat)
        {
            if (id != reservedSeat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservedSeat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservedSeatExists(reservedSeat.Id))
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
            ViewData["ReservetionId"] = new SelectList(_context.Reservetion, "Id", "Status", reservedSeat.ReservetionId);
            ViewData["ScreeningId"] = new SelectList(_context.Screening, "Id", "Id", reservedSeat.ScreeningId);
            ViewData["SeatId"] = new SelectList(_context.Seat, "Id", "Id", reservedSeat.SeatId);
            return View(reservedSeat);
        }

        // GET: ReservedSeats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReservedSeat == null)
            {
                return NotFound();
            }

            var reservedSeat = await _context.ReservedSeat
                .Include(r => r.Reservetion)
                .Include(r => r.Screening)
                .Include(r => r.Seat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservedSeat == null)
            {
                return NotFound();
            }

            return View(reservedSeat);
        }

        // POST: ReservedSeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReservedSeat == null)
            {
                return Problem("Entity set 'NSC_projectContext.ReservedSeat'  is null.");
            }
            var reservedSeat = await _context.ReservedSeat.FindAsync(id);
            if (reservedSeat != null)
            {
                _context.ReservedSeat.Remove(reservedSeat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservedSeatExists(int id)
        {
          return (_context.ReservedSeat?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
