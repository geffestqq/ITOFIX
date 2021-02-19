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
    public class FileTasksController : Controller
    {
        private readonly DiplomContext _context;

        public FileTasksController(DiplomContext context)
        {
            _context = context;
            _context.Zadachis.Load();
        }

        // GET: FileTasks
        public async Task<IActionResult> Index()
        {
            var diplomContext = _context.FileTask.Include(f => f.TaskDistribution);
            return View(await diplomContext.ToListAsync());
        }

        // GET: FileTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTask = await _context.FileTask
                .Include(f => f.TaskDistribution)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileTask == null)
            {
                return NotFound();
            }

            return View(fileTask);
        }

        // GET: FileTasks/Create
        public IActionResult Create()
        {
            ViewData["TaskDistributionId"] = new SelectList(_context.TaskDistributions, "Id", "View");
            return View();
        }

        // POST: FileTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Path_File,File_Detail,TaskDistributionId")] FileTask fileTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskDistributionId"] = new SelectList(_context.TaskDistributions, "Id", "View", fileTask.TaskDistributionId);
            return View(fileTask);
        }

        // GET: FileTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTask = await _context.FileTask.FindAsync(id);
            if (fileTask == null)
            {
                return NotFound();
            }
            ViewData["TaskDistributionId"] = new SelectList(_context.TaskDistributions, "Id", "View", fileTask.TaskDistributionId);
            return View(fileTask);
        }

        // POST: FileTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Path_File,File_Detail,TaskDistributionId")] FileTask fileTask)
        {
            if (id != fileTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileTaskExists(fileTask.Id))
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
            ViewData["TaskDistributionId"] = new SelectList(_context.TaskDistributions, "Id", "View", fileTask.TaskDistributionId);
            return View(fileTask);
        }

        // GET: FileTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTask = await _context.FileTask
                .Include(f => f.TaskDistribution)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileTask == null)
            {
                return NotFound();
            }

            return View(fileTask);
        }

        // POST: FileTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileTask = await _context.FileTask.FindAsync(id);
            _context.FileTask.Remove(fileTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileTaskExists(int id)
        {
            return _context.FileTask.Any(e => e.Id == id);
        }
    }
}
