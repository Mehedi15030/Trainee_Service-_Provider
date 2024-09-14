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
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Results
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Result.Include(r => r.Exam).Include(r => r.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Results/Details/5
        [Authorize(Roles = "Trainer,Co-ordinator,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.Exam)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Topic");
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo");
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,ExamId,StudentId,IsPresent,ObtainedMarks")] Result result)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(result);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Topic", result.ExamId);
        //    ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo", result.StudentId);
        //    return View(result);
        //}
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ExamId)
        {
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Topic");
            if (ExamId <= 0)
            {
                return View();
            }

            var exam = _context.Exam.FirstOrDefault(x => x.Id == ExamId);
            if (exam != null)
            {
                var bstd = _context.StudentBatch.Include(x => x.Batch).Where(x => x.BatchId == exam.BatchId);
                if(bstd!=null && bstd.Count() > 0)
                {
                    foreach (var item in bstd)
                    {
                        Result r = new Result();
                        r.ExamId = ExamId;
                        r.StudentId = item.StudentId;
                        r.ObtainedMarks = 0;
                        _context.Result.Add(r);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Topic", result.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo", result.StudentId);
            return View(result);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExamId,StudentId,IsPresent,ObtainedMarks")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Topic", result.ExamId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo", result.StudentId);
            return View(result);
        }

        // GET: Results/Delete/5
        [Authorize(Roles = "Trainer,Co-ordinator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.Exam)
                .Include(r => r.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Result.FindAsync(id);
            if (result != null)
            {
                _context.Result.Remove(result);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
            return _context.Result.Any(e => e.Id == id);
        }
    }
}
