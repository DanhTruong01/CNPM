using Microsoft.AspNetCore.Mvc;

namespace QBCA.Controllers
{
    public class DuplicateController : Controller
    {
        // GET: /Duplicate/CheckDuplicate
        [HttpGet]
        public IActionResult CheckDuplicate()
        {
            return View();
        }

        // POST: /Duplicate/CheckDuplicate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckDuplicate(string questionText)
        {
            if (string.IsNullOrWhiteSpace(questionText))
            {
                ViewBag.Error = "Please enter an English question to check.";
                return View();
            }

            // TODO: Gọi API Python tại đây và nhận kết quả
            // Hiện tại chỉ giả lập kết quả
            ViewBag.Result = "Duplicate check completed! (This is a mock result.)";

            return View();
        }
    }
}