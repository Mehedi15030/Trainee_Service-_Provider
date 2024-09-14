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
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var batch = _context.Batch.ToList();
            //var applicationDbContext = _context.Attendance.Include(a => a.Schedule).Include(a => a.Student);
            //return View(await applicationDbContext.ToListAsync());
            return View(batch);
        }

        // GET: Attendances/Details/5
        [Authorize(Roles = "Trainer,Co-ordinator,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                .Include(a => a.Schedule)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        [Authorize(Roles = "Trainer,Co-ordinator")]
        public IActionResult Create()
        {
            ViewData["ScheduleId"] = new SelectList(_context.Set<Schedule>(), "Id", "ScheduleName");
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ScheduleId,StudentId,IsPresent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduleId"] = new SelectList(_context.Set<Schedule>(), "Id", "ScheduleName", attendance.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo", attendance.StudentId);
            return View(attendance);
        }

        [Authorize(Roles = "Trainer,Co-ordinator")]
        public IActionResult AttendanceSheet(int batchid)
        {
            var sch = _context.Schedule.Where(x => x.BatchId == batchid);
            return View(sch);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        [HttpPost]
        public async Task<IActionResult> CreateAttendanceSheet(int schid)
        {
            var sch = _context.Schedule.FirstOrDefault(x => x.Id == schid);
            if (sch != null)
            {
                int bid = sch.BatchId;
                var stdlist = _context.StudentBatch.Where(x => x.BatchId == bid).ToList();
                if (stdlist != null && stdlist.Count > 0)
                {
                    foreach (var item in stdlist)
                    {
                        Attendance a = new Attendance() { ScheduleId = schid, StudentId = item.StudentId, IsPresent = false };
                        _context.Attendance.Add(a);
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction("ShowAttendanceSheet", new { schid = schid });
                    //return RedirectToAction("ShowAttendanceSheet");
                }
                return RedirectToAction("AttendanceSheet");
                //return RedirectToAction();

            }

            //return RedirectToAction("AttendanceSheet");
            return RedirectToAction();
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        public IActionResult ShowAttendanceSheet(int schid)
       {
           var att = _context.Attendance.Include(x => x.Student).Where(x => x.ScheduleId == schid);
           return View(att);
       }


        // GET: Attendances/Edit/5
        [Authorize(Roles = "Trainer,Co-ordinator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Set<Schedule>(), "Id", "ScheduleName", attendance.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo", attendance.StudentId);
            return View("prsent");
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ScheduleId,StudentId,IsPresent")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Id))
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
            ViewData["ScheduleId"] = new SelectList(_context.Set<Schedule>(), "Id", "ScheduleName", attendance.ScheduleId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "MobileNo", attendance.StudentId);
            return View();
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]
        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendance
                .Include(a => a.Schedule)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }
        [Authorize(Roles = "Trainer,Co-ordinator")]

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendance.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendance.Any(e => e.Id == id);
        }
     

    }
}
