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
    public class TypeCommentsController : Controller
    {
        private readonly DiplomContext _context;

        public TypeCommentsController(DiplomContext context)
        {
            _context = context;
        }

        // GET: TypeComments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeComments.ToListAsync());
        }

        // GET: TypeComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeComment = await _context.TypeComments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeComment == null)
            {
                return NotFound();
            }

            return View(typeComment);
        }

        // GET: TypeComments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name_comment")] TypeComment typeComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeComment);
        }

        // GET: TypeComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeComment = await _context.TypeComments.FindAsync(id);
            if (typeComment == null)
            {
                return NotFound();
            }
            return View(typeComment);
        }

        // POST: TypeComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name_comment")] TypeComment typeComment)
        {
            if (id != typeComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeCommentExists(typeComment.Id))
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
            return View(typeComment);
        }

        // GET: TypeComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeComment = await _context.TypeComments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeComment == null)
            {
                return NotFound();
            }

            return View(typeComment);
        }

        // POST: TypeComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeComment = await _context.TypeComments.FindAsync(id);
            _context.TypeComments.Remove(typeComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeCommentExists(int id)
        {
            return _context.TypeComments.Any(e => e.Id == id);
        }
    }
}
