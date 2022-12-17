using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWorldCup.Data;
using MVCWorldCup.Models;

namespace MVCWorldCup.Controllers
{
    public class TeamsController : Controller
    {
        private readonly MVCWorldCupContext _context;
        private readonly MVCWorldCupContext _contextGroup;

        public TeamsController(MVCWorldCupContext context, MVCWorldCupContext contextGroup)
        {
            _context = context;
            _contextGroup = contextGroup;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return _context.Team != null ?
                        View(await _context.Team.ToListAsync()) :
                        Problem("Entity set 'MVCWorldCupContext.Team'  is null.");
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamName,TotalMatches,Win,Draw,Lose,Goal,GoalsConceded,Difference,Point,GroupName")] Team team)
        {
            if (!ModelState.IsValid)
            {
                team.TotalMatches = team.Win + team.Draw + team.Lose;
                team.Difference = team.Goal - team.GoalsConceded;
                team.Point = team.Win * 3 + team.Draw * 1;
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamName,TotalMatches,Win,Draw,Lose,Goal,GoalsConceded,Difference,Point,GroupName")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    team.TotalMatches = team.Win + team.Draw + team.Lose;
                    team.Difference = team.Goal - team.GoalsConceded;
                    team.Point = team.Win * 3 + team.Draw * 1;
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Team == null)
            {
                return Problem("Entity set 'MVCWorldCupContext.Team'  is null.");
            }
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return (_context.Team?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
