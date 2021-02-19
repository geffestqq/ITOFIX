using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Diplom.Data;

namespace NewDiplom.Controllers
{
    public class PluralitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PluralitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pluralities
        public async Task<IActionResult> Index()
        {
            var diplomContext = _context.Plurality.Include(p => p.Employee).Include(p => p.Position);
            return View(await diplomContext.ToListAsync());
        }

        // GET: Pluralities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plurality = await _context.Plurality
                .Include(p => p.Employee)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plurality == null)
            {
                return NotFound();
            }

            return View(plurality);
        }

        // GET: Pluralities/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "View");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "View");
            return View();
        }

        // POST: Pluralities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,PositionId")] Plurality plurality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plurality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "View", plurality.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "View", plurality.PositionId);
            return View(plurality);
        }

        // GET: Pluralities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plurality = await _context.Plurality.FindAsync(id);
            if (plurality == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "View", plurality.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "View", plurality.PositionId);
            return View(plurality);
        }

        // POST: Pluralities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,PositionId")] Plurality plurality)
        {
            if (id != plurality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plurality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PluralityExists(plurality.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "View", plurality.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "View", plurality.PositionId);
            return View(plurality);
        }

        // GET: Pluralities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plurality = await _context.Plurality
                .Include(p => p.Employee)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plurality == null)
            {
                return NotFound();
            }

            return View(plurality);
        }

        // POST: Pluralities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plurality = await _context.Plurality.FindAsync(id);
            _context.Plurality.Remove(plurality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PluralityExists(int id)
        {
            return _context.Plurality.Any(e => e.Id == id);
        }
    }
}
