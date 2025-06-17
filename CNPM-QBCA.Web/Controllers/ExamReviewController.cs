using QBCA.Data;
using Microsoft.AspNetCore.Mvc;
using QBCA.Models;
using System.Linq;

namespace CNPM_QBCA.Web.Controllers
{
    public class ExamReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  Hiển thị danh sách bài cần review
        public IActionResult ReviewExam()
        {
            var exams = _context.ReviewExams
                .OrderByDescending(e => e.SubmittedDate)
                .ToList();

            return View(exams);
        }

        //  Hiển thị chi tiết bài thi
        public IActionResult ReviewDetails(int id)
        {
            var exam = _context.ReviewExams.FirstOrDefault(e => e.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }
    }
}
