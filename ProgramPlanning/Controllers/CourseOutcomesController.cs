using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgramPlanning.Data;
using ProgramPlanning.Models;

namespace ProgramPlanning.Controllers
{
    public class CourseOutcomesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseOutcomesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseOutcomes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseOutcomes.Include(c => c.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseOutcomes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseOutcome = await _context.CourseOutcomes
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseOutcome == null)
            {
                return NotFound();
            }

            return View(courseOutcome);
        }

        // GET: CourseOutcomes/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Prefix");
            return View();
        }

        // POST: CourseOutcomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,Outcome")] CourseOutcome courseOutcome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseOutcome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Prefix", courseOutcome.CourseId);
            return View(courseOutcome);
        }

        // GET: CourseOutcomes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseOutcome = await _context.CourseOutcomes.FindAsync(id);
            if (courseOutcome == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Prefix", courseOutcome.CourseId);
            return View(courseOutcome);
        }

        // POST: CourseOutcomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,Outcome")] CourseOutcome courseOutcome)
        {
            if (id != courseOutcome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseOutcome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseOutcomeExists(courseOutcome.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Prefix", courseOutcome.CourseId);
            return View(courseOutcome);
        }

        // GET: CourseOutcomes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseOutcome = await _context.CourseOutcomes
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseOutcome == null)
            {
                return NotFound();
            }

            return View(courseOutcome);
        }

        // POST: CourseOutcomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseOutcome = await _context.CourseOutcomes.FindAsync(id);
            _context.CourseOutcomes.Remove(courseOutcome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseOutcomeExists(int id)
        {
            return _context.CourseOutcomes.Any(e => e.Id == id);
        }
    }
}
