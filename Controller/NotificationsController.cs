using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using QBCA.Data;
using Microsoft.EntityFrameworkCore;

namespace QBCA.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Notifications/Notifications
        public async Task<IActionResult> Notifications()
        {
            var userIdStr = User.FindFirst("UserID")?.Value;
            if (!int.TryParse(userIdStr, out int userId))
                return RedirectToAction("Unauthorized", "Home");

            var notifications = await _context.Notifications
                .Include(n => n.CreatedByUser)
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View("Notifications", notifications);
        }

        // POST: /Notifications/MarkAsReadOrUnread
        [HttpPost]
        public async Task<IActionResult> MarkAsReadOrUnread(int id, string status)
        {
            var userIdStr = User.FindFirst("UserID")?.Value;
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationID == id && n.UserID == userId);
            if (notification == null) return NotFound();

            // Always store and return status as UPPER CASE: "READ" or "UNREAD"
            string newStatus = (status ?? "").ToUpper();
            notification.Status = newStatus == "UNREAD" ? "UNREAD" : "READ";
            await _context.SaveChangesAsync();
            return Json(new { success = true, status = notification.Status });
        }
    }
}