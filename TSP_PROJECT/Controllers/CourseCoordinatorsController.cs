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
    public class CourseCoordinatorsController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public CourseCoordinatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseCoordinators
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseCoordinator.ToListAsync());
        }

        // GET: CourseCoordinators/Details/5
        [Authorize(Roles = "Trainer,Co-ordinator,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCoordinator = await _context.CourseCoordinator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCoordinator == null)
            {
                return NotFound();
            }

            return View(courseCoordinator);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: CourseCoordinators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseCoordinators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MobileNo,Email")] CourseCoordinator courseCoordinator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseCoordinator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseCoordinator);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: CourseCoordinators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCoordinator = await _context.CourseCoordinator.FindAsync(id);
            if (courseCoordinator == null)
            {
                return NotFound();
            }
            return View(courseCoordinator);
        }

        // POST: CourseCoordinators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MobileNo,Email")] CourseCoordinator courseCoordinator)
        {
            if (id != courseCoordinator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseCoordinator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseCoordinatorExists(courseCoordinator.Id))
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
            return View(courseCoordinator);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: CourseCoordinators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCoordinator = await _context.CourseCoordinator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseCoordinator == null)
            {
                return NotFound();
            }

            return View(courseCoordinator);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: CourseCoordinators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseCoordinator = await _context.CourseCoordinator.FindAsync(id);
            if (courseCoordinator != null)
            {
                _context.CourseCoordinator.Remove(courseCoordinator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseCoordinatorExists(int id)
        {
            return _context.CourseCoordinator.Any(e => e.Id == id);
        }
    }
}
