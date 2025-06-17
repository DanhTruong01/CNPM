using Microsoft.AspNetCore.Mvc;
using QBCA.Data;
using QBCA.Models;
using System;
using System.Linq;

namespace CNPM_QBCA.Web.Controllers
{
    public class ExamApproveController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamApproveController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var examTasks = _context.ExamApproveTasks
                .OrderByDescending(t => t.LastUpdated)
                .ToList();
            return View(examTasks);
        }

        public IActionResult Review(int? id)
        {
            ExamApproveTask? examTask;

            if (id == null)
            {
                examTask = _context.ExamApproveTasks
                    .OrderByDescending(t => t.LastUpdated)
                    .FirstOrDefault();
            }
            else
            {
                examTask = _context.ExamApproveTasks
                    .FirstOrDefault(t => t.Id == id.Value);
            }

            if (examTask == null)
            {
                ViewBag.NoData = true;
                return View(new ExamApproveTask { /* default fields */ });
            }

            ViewBag.NoData = false;
            return View(examTask);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveUpdate(ExamApproveTask updatedTask)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedTask);
            }

            var examTask = _context.ExamApproveTasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (examTask == null)
            {
                return NotFound();
            }

            examTask.Status = updatedTask.Status;
            examTask.Feedback = updatedTask.Feedback;
            examTask.LastUpdated = DateTime.Now;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
