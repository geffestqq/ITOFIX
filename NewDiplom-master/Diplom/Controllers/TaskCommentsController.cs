using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Diplom.Data;

namespace Diplom.Controllers
{
    public class TaskCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskCommentsController(ApplicationDbContext context)
        {
            _context = context;
            _context.Zadachis.Load();
        }

        // GET: TaskComments
        public async Task<IActionResult> Index()
        {
            var diplomContext = _context.TaskComments.Include(t => t.TaskDistributions).Include(t => t.Type_Comments);
            return View(await diplomContext.ToListAsync());
        }

        // GET: TaskComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskComment = await _context.TaskComments
                .Include(t => t.TaskDistributions)
                .Include(t => t.Type_Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskComment == null)
            {
                return NotFound();
            }

            return View(taskComment);
        }

        // GET: TaskComments/Create
        public IActionResult Create()
        {
            ViewData["TaskDistributionsId"] = new SelectList(_context.TaskDistributions, "Id", "View");
            ViewData["Type_CommentsId"] = new SelectList(_context.TypeComments, "Id", "Name_comment");
            return View();
        }

        // POST: TaskComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text_comment,Type_CommentsId,TaskDistributionsId")] TaskComment taskComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskDistributionsId"] = new SelectList(_context.TaskDistributions, "Id", "View", taskComment.TaskDistributionsId);
            ViewData["Type_CommentsId"] = new SelectList(_context.TypeComments, "Id", "Name_comment", taskComment.Type_CommentsId);
            return View(taskComment);
        }

        // GET: TaskComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskComment = await _context.TaskComments.FindAsync(id);
            if (taskComment == null)
            {
                return NotFound();
            }
            ViewData["TaskDistributionsId"] = new SelectList(_context.TaskDistributions, "Id", "View", taskComment.TaskDistributionsId);
            ViewData["Type_CommentsId"] = new SelectList(_context.TypeComments, "Id", "Name_comment", taskComment.Type_CommentsId);
            return View(taskComment);
        }

        // POST: TaskComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text_comment,Type_CommentsId,TaskDistributionsId")] TaskComment taskComment)
        {
            if (id != taskComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskCommentExists(taskComment.Id))
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
            ViewData["TaskDistributionsId"] = new SelectList(_context.TaskDistributions, "Id", "View", taskComment.TaskDistributionsId);
            ViewData["Type_CommentsId"] = new SelectList(_context.TypeComments, "Id", "Name_comment", taskComment.Type_CommentsId);
            return View(taskComment);
        }

        // GET: TaskComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskComment = await _context.TaskComments
                .Include(t => t.TaskDistributions)
                .Include(t => t.Type_Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskComment == null)
            {
                return NotFound();
            }

            return View(taskComment);
        }

        // POST: TaskComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskComment = await _context.TaskComments.FindAsync(id);
            _context.TaskComments.Remove(taskComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskCommentExists(int id)
        {
            return _context.TaskComments.Any(e => e.Id == id);
        }
    }
}
