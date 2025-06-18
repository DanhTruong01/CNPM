using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QBCA.Data;
using QBCA.Models;
using System.Linq;

namespace CNPM_QBCA.Web.Controllers
{
    [Route("ExamTasks")]
    public class ExamTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /ExamTasks/AssignedTasks
        [HttpGet("AssignedTasks")]
        public IActionResult AssignedTasks()
        {
            var assignments = _context.ExamAssignments.ToList();
            return View("AssignedTasks", assignments);
        }

        // GET: /ExamTasks/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(); // Views/ExamTasks/Create.cshtml
        }

        // POST: /ExamTasks/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExamAssignment assignment)
        {
            if (ModelState.IsValid)
            {
                assignment.IsReviewed = false;
                _context.ExamAssignments.Add(assignment);
                _context.SaveChanges();

                TempData["Success"] = "Task assigned successfully!";
                return RedirectToAction("AssignedTasks");
            }

            return View(assignment);
        }

        // GET: /ExamTasks/Review/5
        [HttpGet("Review/{id}")]
        public IActionResult Review(int id)
        {
            var assignment = _context.ExamAssignments.FirstOrDefault(a => a.Id == id);
            if (assignment == null)
                return NotFound();

            return View("Review", assignment);
        }

        // POST: /ExamTasks/Review/5
        [HttpPost("Review/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Review(int id, string comments)
        {
            var assignment = _context.ExamAssignments.FirstOrDefault(a => a.Id == id);
            if (assignment == null)
                return NotFound();

            assignment.Comments = comments;
            assignment.IsReviewed = true;
            _context.SaveChanges();

            TempData["Success"] = "Assignment reviewed successfully!";
            return RedirectToAction("AssignedTasks");
        }
    }
}
