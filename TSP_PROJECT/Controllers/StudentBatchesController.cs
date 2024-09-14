using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TSP_PROJECT.Data;
using TSP_PROJECT.Models;

namespace TSP_PROJECT.Controllers
{
    [Authorize]
    public class StudentBatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentBatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentBatches
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentBatch.Include(s => s.Batch).Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentBatches/Details/5
        [Authorize(Roles = "Trainer,Co-ordinator,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentBatch = await _context.StudentBatch
                .Include(s => s.Batch)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentBatch == null)
            {
                return NotFound();
            }

            return View(studentBatch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: StudentBatches/Create
        public IActionResult Create()
        {
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "StudentName");
            return View();
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: StudentBatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BatchId,StudentId")] StudentBatch studentBatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentBatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName", studentBatch.BatchId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "StudentName", studentBatch.StudentId);
            return View(studentBatch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: StudentBatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentBatch = await _context.StudentBatch.FindAsync(id);
            if (studentBatch == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName", studentBatch.BatchId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "StudentName", studentBatch.StudentId);
            return View(studentBatch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: StudentBatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BatchId,StudentId")] StudentBatch studentBatch)
        {
            if (id != studentBatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentBatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentBatchExists(studentBatch.Id))
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
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName", studentBatch.BatchId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "StudentName", studentBatch.StudentId);
            return View(studentBatch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: StudentBatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentBatch = await _context.StudentBatch
                .Include(s => s.Batch)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentBatch == null)
            {
                return NotFound();
            }

            return View(studentBatch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: StudentBatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentBatch = await _context.StudentBatch.FindAsync(id);
            if (studentBatch != null)
            {
                _context.StudentBatch.Remove(studentBatch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentBatchExists(int id)
        {
            return _context.StudentBatch.Any(e => e.Id == id);
        }
    }
}
