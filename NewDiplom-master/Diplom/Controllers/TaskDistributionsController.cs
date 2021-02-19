using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;
using Diplom.Enums;
using Diplom.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Diplom.Controllers
{
    [Authorize]
    public class TaskDistributionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public TaskDistributionsController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: TaskDistributions
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (await _userManager.IsInRoleAsync(user, "админ"))
                {
                    var _tasks = _context.TaskDistributions
                        .Include(e => e.Plurality)
                        .Include(e => e.Plurality.Employee)
                        .Include(e => e.Plurality.Position)
                        .Include(e => e.Zadachi)
                        .Include(e => e.Status)
                        .ToList();
                    return View(_tasks);
                }

                var tasks = _context.TaskDistributions
                        .Include(e => e.Plurality)
                        .Include(e => e.Plurality.Employee)
                        .Include(e => e.Plurality.Position)
                        .Include(e => e.Zadachi)
                        .Include(e => e.Status)
                        .Where(e => e.PluralityId == user.PluralityId).ToList();

                return View(tasks);
            }
            return View();
        }

        // GET: TaskDistributions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = _userManager.Users.Include(e => e.Plurality.TaskDistributions)
                .Include(e => e.Plurality.Position)
                .Include(e => e.Plurality.Employee)
                .FirstOrDefaultAsync(e => e.Plurality != null);

            var taskDistribution = await _context.TaskDistributions
                .Include(t => t.Plurality)
                .Include(t => t.Status)
                .Include(t => t.Zadachi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskDistribution == null)
            {
                return NotFound();
            }

            _context.Zadachis.Load();
            _context.Employees.Load();
            _context.Positions.Load();
            return View(taskDistribution);
        }

        // GET: TaskDistributions/Create
        public IActionResult Create()
        {
            _context.Zadachis.Load();
            _context.Employees.Load();
            _context.Positions.Load();
            ViewData["PluralityId"] = new SelectList(_context.Plurality, "Id", "View");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name");
            ViewData["ZadachiId"] = new SelectList(_context.Zadachis, "Id", "View");
            return View();
        }

        // POST: TaskDistributions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartedAt,PeriodUnit,PeriodValue,PluralityId,ZadachiId,StatusId")] TaskDistribution taskDistribution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskDistribution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _context.Zadachis.Load();
            _context.Employees.Load();
            _context.Positions.Load();
            ViewData["PluralityId"] = new SelectList(_context.Plurality, "Id", "View", taskDistribution.PluralityId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name", taskDistribution.StatusId);
            ViewData["ZadachiId"] = new SelectList(_context.Zadachis, "Id", "View", taskDistribution.ZadachiId);
            return View(taskDistribution);
        }

        // GET: TaskDistributions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskDistribution = await _context.TaskDistributions.FindAsync(id);
            if (taskDistribution == null)
            {
                return NotFound();
            }
            _context.Zadachis.Load();
            _context.Employees.Load();
            _context.Positions.Load();
            ViewData["PluralityId"] = new SelectList(_context.Plurality, "Id", "View", taskDistribution.PluralityId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name", taskDistribution.StatusId);
            ViewData["ZadachiId"] = new SelectList(_context.Zadachis, "Id", "View", taskDistribution.ZadachiId);
            return View(taskDistribution);
        }

        // POST: TaskDistributions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartedAt,PeriodUnit,PeriodValue,PluralityId,ZadachiId,StatusId")] TaskDistribution taskDistribution)
        {
            if (id != taskDistribution.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskDistribution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskDistributionExists(taskDistribution.Id))
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
            _context.Zadachis.Load();
            _context.Employees.Load();
            _context.Positions.Load();
            ViewData["PluralityId"] = new SelectList(_context.Plurality, "Id", "View", taskDistribution.PluralityId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Status_name", taskDistribution.StatusId);
            ViewData["ZadachiId"] = new SelectList(_context.Zadachis, "Id", "View", taskDistribution.ZadachiId);
            return View(taskDistribution);
        }

        // GET: TaskDistributions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskDistribution = await _context.TaskDistributions
                .Include(t => t.Plurality)
                .Include(t => t.Status)
                .Include(t => t.Zadachi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskDistribution == null)
            {
                return NotFound();
            }

            _context.Zadachis.Load();
            _context.Employees.Load();
            _context.Positions.Load();

            return View(taskDistribution);
        }

        // POST: TaskDistributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskDistribution = await _context.TaskDistributions.FindAsync(id);
            _context.TaskDistributions.Remove(taskDistribution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskDistributionExists(int id)
        {
            return _context.TaskDistributions.Any(e => e.Id == id);
        }
        [HttpGet]
        public IActionResult FreeEmployees()
        {
            var now = DateTime.Now;
            _context.TaskDistributions.Load();
            _context.Plurality.Load();
            _context.Employees.Load();

            var countAll = _context.Employees
                .Count();
            var usedEList = _context.TaskDistributions
                .Where(s => s.StartedAt < now)
                .ToList();
            for (int i = usedEList.Count - 1; i > -1; i--)
            {
                if (!CheckDate(now, usedEList[i].StartedAt, usedEList[i].PeriodUnit, usedEList[i].PeriodValue))
                    usedEList.RemoveAt(i);
            }
            var countUsed = usedEList
                .Select(s => s.Plurality.Employee)
                .Distinct()
                .Count();
            var countFree = countAll - countUsed;
            ViewData["CountAll"] = countAll;
            ViewData["CountUsed"] = countUsed;
            ViewData["CountFree"] = countFree;
            if (countAll == 0)
                ViewData["Percent"] = 0;
            else
                ViewData["Percent"] = (int)(100 * countUsed / countAll);
            return View();
        }

        private bool CheckDate(DateTime now, DateTime startedAt, PeriodUnitEnum periodUnit, int periodValue)
        {
            var result = false;
            DateTime EndDate = new DateTime();
            switch (periodUnit)
            {
                case PeriodUnitEnum.Day:
                    EndDate = startedAt.AddDays(periodValue);
                    break;
                case PeriodUnitEnum.Week:
                    EndDate = startedAt.AddDays(7 * periodValue);
                    break;
                case PeriodUnitEnum.Month:
                    EndDate = startedAt.AddMonths(periodValue);
                    break;
            }
            result = EndDate > now;
            return result;
        }
    }
}
