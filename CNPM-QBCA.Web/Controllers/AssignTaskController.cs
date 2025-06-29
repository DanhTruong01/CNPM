using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QBCA.Data;
using QBCA.Models;
using QBCA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QBCA.Controllers
{
    public class AssignTaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Task(string assignedTo, string status, string keyword)
        {
            var tasksQuery = _context.TaskAssignments
                .Include(t => t.Assignee)
                .AsQueryable();

            if (!string.IsNullOrEmpty(assignedTo))
                tasksQuery = tasksQuery.Where(t => t.Assignee.FullName == assignedTo);

            if (!string.IsNullOrEmpty(status))
                tasksQuery = tasksQuery.Where(t => t.Status == status);

            if (!string.IsNullOrEmpty(keyword))
                tasksQuery = tasksQuery.Where(t => t.TaskType.Contains(keyword));

            var model = tasksQuery.Select(t => new AssignTaskViewModel
            {
                TaskID = t.AssignmentID,
                TaskType = t.TaskType,
                AssignedToName = t.Assignee.FullName,
                AssignedAt = t.AssignedAt,
                Deadline = t.DueDate ?? DateTime.MinValue,
                Status = t.Status
            }).ToList();

            ViewBag.AllLecturers = _context.Users
                .Where(u => u.Role.RoleName == "Lecturer" || u.Role.RoleName == "Subject Leader")
                .Select(u => u.FullName)
                .Distinct().ToList();

            ViewBag.StatusOptions = new List<string> { "Assigned", "InProgress", "Completed" };
            ViewBag.CurrentFilter = assignedTo;
            ViewBag.CurrentStatus = status;
            ViewBag.Keyword = keyword;

            return View(model);
        }

        // GET: AssignTask/Create
        public IActionResult Create()
        {
            var model = new AssignTaskViewModel
            {
                AssignedAt = DateTime.Now,
                Deadline = DateTime.Now.AddDays(7),
                AllLecturers = GetEligibleLecturers()
            };
            return View(model);
        }

        // POST: AssignTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AssignTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = new TaskAssignment
                {
                    TaskType = model.TaskType,
                    AssignedTo = model.AssignedToID,
                    AssignedBy = 1, // TODO: Replace with logged-in user ID
                    AssignedAt = DateTime.Now,
                    DueDate = model.Deadline,
                    Status = "Assigned",
                    CreatedAt = DateTime.Now
                };

                _context.TaskAssignments.Add(task);
                _context.SaveChanges();

                // 🎯 Gửi thông báo đến giảng viên
                var notification = new Notification
                {
                    UserID = model.AssignedToID,
                    Message = $"📝 You have been assigned a task: {model.TaskType} (Deadline: {model.Deadline:dd/MM/yyyy})",
                    CreatedAt = DateTime.Now,
                    CreatedBy = task.AssignedBy,
                    Status = "Unread",
                    RelatedEntityType = "AssignTask",
                    RelatedEntityID = task.AssignmentID
                };

                _context.Notifications.Add(notification);
                _context.SaveChanges();

                return RedirectToAction(nameof(Task));
            }

            model.AllLecturers = GetEligibleLecturers();
            return View(model);
        }

        // GET: AssignTask/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _context.TaskAssignments.Find(id);
            if (task == null)
                return NotFound();

            var model = new AssignTaskViewModel
            {
                TaskID = task.AssignmentID,
                TaskType = task.TaskType,
                AssignedToID = task.AssignedTo,
                Deadline = task.DueDate ?? DateTime.MinValue,
                Status = task.Status,
                AllLecturers = GetEligibleLecturers()
            };

            return View(model);
        }

        // POST: AssignTask/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AssignTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = _context.TaskAssignments.Find(model.TaskID);
                if (task == null)
                    return NotFound();

                task.TaskType = model.TaskType;
                task.AssignedTo = model.AssignedToID;
                task.DueDate = model.Deadline;
                task.Status = model.Status;

                _context.SaveChanges();
                return RedirectToAction(nameof(Task));
            }

            model.AllLecturers = GetEligibleLecturers();
            return View(model);
        }

        // GET: AssignTask/Details/5
        public IActionResult Details(int id)
        {
            var task = _context.TaskAssignments
                .Include(t => t.Assignee)
                .Include(t => t.Assigner)
                .FirstOrDefault(t => t.AssignmentID == id);

            if (task == null)
                return NotFound();

            var model = new AssignTaskViewModel
            {
                TaskID = task.AssignmentID,
                TaskType = task.TaskType,
                AssignedToName = task.Assignee?.FullName,
                AssignedByID = task.AssignedBy,
                Deadline = task.DueDate ?? DateTime.MinValue,
                AssignedAt = task.AssignedAt,
                Status = task.Status
            };

            return View(model);
        }

        // GET: AssignTask/Delete/5
        public IActionResult Delete(int id)
        {
            var task = _context.TaskAssignments
                .Include(t => t.Assignee)
                .FirstOrDefault(t => t.AssignmentID == id);

            if (task == null)
                return NotFound();

            var model = new AssignTaskViewModel
            {
                TaskID = task.AssignmentID,
                TaskType = task.TaskType,
                AssignedToName = task.Assignee?.FullName,
                Deadline = task.DueDate ?? DateTime.MinValue
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int TaskID)
        {
            var task = _context.TaskAssignments.Find(TaskID);
            if (task == null)
                return NotFound();

            _context.TaskAssignments.Remove(task);
            _context.SaveChanges();

            return RedirectToAction(nameof(Task));
        }

        // Private helper method
        private List<User> GetEligibleLecturers()
        {
            return _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.RoleName == "Lecturer" || u.Role.RoleName == "Subject Leader")
                .ToList();
        }
    }
}
