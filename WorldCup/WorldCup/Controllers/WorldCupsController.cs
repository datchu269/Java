using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorldCup.Data;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    public class WorldCupsController : Controller
    {
        private readonly WorldCupContext _context;

        public WorldCupsController(WorldCupContext context)
        {
            _context = context;
        }

        // GET: WorldCups
        public async Task<IActionResult> Index()
        {
              return _context.WorldCup != null ? 
                          View(await _context.WorldCup.ToListAsync()) :
                          Problem("Entity set 'WorldCupContext.WorldCup'  is null.");
        }

        // GET: WorldCups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorldCup == null)
            {
                return NotFound();
            }

            var worldCup = await _context.WorldCup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worldCup == null)
            {
                return NotFound();
            }

            return View(worldCup);
        }

        // GET: WorldCups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorldCups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Team,Win,Draw,Lose,Goal,GoalsConceded,Difference,Point")] WorldCup worldCup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worldCup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(worldCup);
        }

        // GET: WorldCups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorldCup == null)
            {
                return NotFound();
            }

            var worldCup = await _context.WorldCup.FindAsync(id);
            if (worldCup == null)
            {
                return NotFound();
            }
            return View(worldCup);
        }

        // POST: WorldCups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Team,Win,Draw,Lose,Goal,GoalsConceded,Difference,Point")] WorldCup worldCup)
        {
            if (id != worldCup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worldCup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorldCupExists(worldCup.Id))
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
            return View(worldCup);
        }

        // GET: WorldCups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorldCup == null)
            {
                return NotFound();
            }

            var worldCup = await _context.WorldCup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worldCup == null)
            {
                return NotFound();
            }

            return View(worldCup);
        }

        // POST: WorldCups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorldCup == null)
            {
                return Problem("Entity set 'WorldCupContext.WorldCup'  is null.");
            }
            var worldCup = await _context.WorldCup.FindAsync(id);
            if (worldCup != null)
            {
                _context.WorldCup.Remove(worldCup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorldCupExists(int id)
        {
          return (_context.WorldCup?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
