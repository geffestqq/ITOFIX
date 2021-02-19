using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewDiplom.Models;

namespace NewDiplom.Controllers
{
    public class RightsController : Controller
    {
        private readonly DiplomContext _context;

        public RightsController(DiplomContext context)
        {
            _context = context;
        }

        // GET: Rights
        public async Task<IActionResult> Index()
        {
            var diplomContext = _context.Rights.Include(r => r.Function).Include(r => r.Role);
            return View(await diplomContext.ToListAsync());
        }

        // GET: Rights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var right = await _context.Rights
                .Include(r => r.Function)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (right == null)
            {
                return NotFound();
            }

            return View(right);
        }

        // GET: Rights/Create
        public IActionResult Create()
        {
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Name_function");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name_Role");
            return View();
        }

        // POST: Rights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Give_out,RoleId,FunctionId")] Right right)
        {
            if (ModelState.IsValid)
            {
                _context.Add(right);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Name_function", right.FunctionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name_Role", right.RoleId);
            return View(right);
        }

        // GET: Rights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var right = await _context.Rights.FindAsync(id);
            if (right == null)
            {
                return NotFound();
            }
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Name_function", right.FunctionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name_Role", right.RoleId);
            return View(right);
        }

        // POST: Rights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Give_out,RoleId,FunctionId")] Right right)
        {
            if (id != right.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(right);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RightExists(right.Id))
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
            ViewData["FunctionId"] = new SelectList(_context.Functions, "Id", "Name_function", right.FunctionId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name_Role", right.RoleId);
            return View(right);
        }

        // GET: Rights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var right = await _context.Rights
                .Include(r => r.Function)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (right == null)
            {
                return NotFound();
            }

            return View(right);
        }

        // POST: Rights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var right = await _context.Rights.FindAsync(id);
            _context.Rights.Remove(right);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RightExists(int id)
        {
            return _context.Rights.Any(e => e.Id == id);
        }
    }
}
