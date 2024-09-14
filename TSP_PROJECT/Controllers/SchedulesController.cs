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
    public class SchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Schedules
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Schedule.Include(s => s.Batch).Include(s => s.Lab);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Batch)
                .Include(s => s.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName");
            ViewData["LabId"] = new SelectList(_context.Lab, "Id", "LabName");
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,BatchId,LabId,ScheduleName,ClassDate,FromTime,ToTime,Duration")] Schedule schedule)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(schedule);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName", schedule.BatchId);
        //    ViewData["LabId"] = new SelectList(_context.Lab, "Id", "LabName", schedule.LabId);
        //    return View(schedule);
        //}
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BatchId,LabId,ScheduleName,ClassDate,FromTime,ToTime,Duration")] Schedule schedule)
        {
            int total_class = 0;

            if (ModelState.IsValid)
            {
                int bid = schedule.BatchId;
                var batch_course = _context.Batch.Include(x => x.Course).FirstOrDefault(x => x.Id == bid);
                if(batch_course!=null)
                {
                    double total_hour = batch_course.Course.TotalHours;
                    double class_hour = batch_course.Course.ClassDuration;
                    total_class = (int) Math.Ceiling( total_hour / class_hour);
                }

                DateTime prev_date = schedule.ClassDate;
                int gap = 1;

                for (int i = 1; i <= total_class; i++)
                {
                    Schedule s = new Schedule();
                    
                    if (i % 2 == 1)
                    {
                        gap = 1;
                    }
                    else { gap = 6; }


                    s.BatchId= schedule.BatchId;
                    s.LabId = schedule.LabId;
                    s.ScheduleName= "Class-" + i.ToString().PadLeft(2,'0') ;
                    s.ClassDate  = prev_date;
                    s.FromTime   = schedule.FromTime  ;
                    s.ToTime     = schedule.ToTime  ;
                    s.Duration = schedule.Duration;

                    prev_date = prev_date.AddDays(gap);

                    _context.Add(s);
                }                    


                //_context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName", schedule.BatchId);
            ViewData["LabId"] = new SelectList(_context.Lab, "Id", "LabName", schedule.LabId);
            return View(schedule);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName", schedule.BatchId);
            ViewData["LabId"] = new SelectList(_context.Lab, "Id", "LabName", schedule.LabId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BatchId,LabId,ScheduleName,ClassDate,FromTime,ToTime,Duration")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            ViewData["BatchId"] = new SelectList(_context.Batch, "Id", "BatchName", schedule.BatchId);
            ViewData["LabId"] = new SelectList(_context.Lab, "Id", "LabName", schedule.LabId);
            return View(schedule);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Batch)
                .Include(s => s.Lab)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
}
