using CNPM_QBCA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QBCA.Data;
using QBCA.Models;

public class ExamApproveTaskController : Controller
{
    private readonly ApplicationDbContext _context;
    private int currentUserId;

    public ExamApproveTaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _context.ExamApproveTasks
            .Include(e => e.Exam)
            .Include(e => e.AssignedBy)
            .Where(t => t.AssignedToUserID == currentUserId) // hoặc để hiển thị hết nếu là admin
            .ToListAsync();

        var viewModel = tasks.Select(t => new ApproveExamTaskViewModel
        {
            TaskID = t.ExamApproveTaskID,
            ExamTitle = t.Exam.Title,
            Status = t.Status,
            AssignedBy = t.AssignedBy.FullName,
            AssignedDate = t.AssignedDate,
            CreatedAt = t.CreatedAt
        }).ToList();

        return View(viewModel);
    }


    public async Task<IActionResult> Details(int id)
    {
        var task = await _context.ExamApproveTasks
            .Include(e => e.Exam)
            .Include(e => e.AssignedTo)
            .Include(e => e.AssignedBy)
            .FirstOrDefaultAsync(t => t.ExamApproveTaskID == id);

        if (task == null)
            return NotFound();

        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id, string feedback)
    {
        var task = await _context.ExamApproveTasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.Status = "Approved";
        task.Feedback = feedback;
        task.ApprovedDate = DateTime.Now;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id, string feedback)
    {
        var task = await _context.ExamApproveTasks.FindAsync(id);
        if (task == null)
            return NotFound();

        task.Status = "Rejected";
        task.Feedback = feedback;
        task.ApprovedDate = DateTime.Now;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
