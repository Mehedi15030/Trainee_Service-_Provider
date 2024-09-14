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
    public class CourseContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseContents
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseContent.Include(c => c.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseContents/Details/5
        [Authorize(Roles ="Trainer,Co-ordinator,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseContent = await _context.CourseContent
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseContent == null)
            {
                return NotFound();
            }

            return View(courseContent);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: CourseContents/Create

        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName");
            return View();
        }

        // POST: CourseContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,ContentTitle,ContentDetail,SortOrder")] CourseContent courseContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseContent.CourseId);
            return View(courseContent);
        }

        // GET: CourseContents/Edit/5
        [Authorize(Roles = "Trainer,Co-ordinator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseContent = await _context.CourseContent.FindAsync(id);
            if (courseContent == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseContent.CourseId);
            return View(courseContent);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: CourseContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,ContentTitle,ContentDetail,SortOrder")] CourseContent courseContent)
        {
            if (id != courseContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseContentExists(courseContent.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "CourseName", courseContent.CourseId);
            return View(courseContent);
        }

        // GET: CourseContents/Delete/5
        [Authorize(Roles = "Trainer,Co-ordinator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseContent = await _context.CourseContent
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseContent == null)
            {
                return NotFound();
            }

            return View(courseContent);
        }

        // POST: CourseContents/Delete/5
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseContent = await _context.CourseContent.FindAsync(id);
            if (courseContent != null)
            {
                _context.CourseContent.Remove(courseContent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseContentExists(int id)
        {
            return _context.CourseContent.Any(e => e.Id == id);
        }
    }
}
