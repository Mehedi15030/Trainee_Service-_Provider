using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TSP_PROJECT.Data;
using TSP_PROJECT.Models;
using TSP_PROJECT.Data;
using TSP_PROJECT.Models;
using Microsoft.AspNetCore.Authorization;

namespace TSP_PROJECT.Controllers
{
    [Authorize]
    public class BatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BatchesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Batches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Batch.Include(b => b.Course).Include(b => b.CourseCoordinator).Include(b => b.Trainer);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Trainer,Co-ordinator,Student")]
        // GET: Batches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batch
                .Include(b => b.Course)
                .Include(b => b.CourseCoordinator)
                .Include(b => b.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Batches/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Set<Course>(), "Id", "CourseName");
            ViewData["CourseCoordinatorId"] = new SelectList(_context.Set<CourseCoordinator>(), "Id", "Name");
            ViewData["TrainerId"] = new SelectList(_context.Set<Trainer>(), "Id", "TrainerName");
            return View();
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: Batches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,TrainerId,BatchName,BatchShortName,CourseCoordinatorId")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(batch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Set<Course>(), "Id", "CourseName", batch.CourseId);
            ViewData["CourseCoordinatorId"] = new SelectList(_context.Set<CourseCoordinator>(), "Id", "Name", batch.CourseCoordinatorId);
            ViewData["TrainerId"] = new SelectList(_context.Set<Trainer>(), "Id", "TrainerName", batch.TrainerId);
            return View(batch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Batches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batch.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Set<Course>(), "Id", "CourseName", batch.CourseId);
            ViewData["CourseCoordinatorId"] = new SelectList(_context.Set<CourseCoordinator>(), "Id", "Name", batch.CourseCoordinatorId);
            ViewData["TrainerId"] = new SelectList(_context.Set<Trainer>(), "Id", "TrainerName", batch.TrainerId);
            return View(batch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: Batches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,TrainerId,BatchName,BatchShortName,CourseCoordinatorId")] Batch batch)
        {
            if (id != batch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatchExists(batch.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Set<Course>(), "Id", "CourseName", batch.CourseId);
            ViewData["CourseCoordinatorId"] = new SelectList(_context.Set<CourseCoordinator>(), "Id", "Name", batch.CourseCoordinatorId);
            ViewData["TrainerId"] = new SelectList(_context.Set<Trainer>(), "Id", "TrainerName", batch.TrainerId);
            return View(batch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Batches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var batch = await _context.Batch
                .Include(b => b.Course)
                .Include(b => b.CourseCoordinator)
                .Include(b => b.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (batch == null)
            {
                return NotFound();
            }

            return View(batch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var batch = await _context.Batch.FindAsync(id);
            if (batch != null)
            {
                _context.Batch.Remove(batch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BatchExists(int id)
        {
            return _context.Batch.Any(e => e.Id == id);
        }
    }
}
