using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CNPM_QBCA.Models;

namespace CNPM_QBCA.Controllers
{
    [Route("Exam")]
    public class MockExamController : Controller
    {
        // MOCK DATA for demo (replace with DbContext later)
        private static List<MockExamViewModel> mockExams = new List<MockExamViewModel>
        {
            new MockExamViewModel
            {
                //ExamId = 1,
                //Title = "Midterm Exam Review - Semester 1",
                //Instructions = "Read each question carefully and select the correct answer.",
                //Deadline = new DateTime(2025, 6, 13),
                //Status = "Pending",
                //AssignedBy = "Department Head",
                Questions = new List<MockQuestion>
                {
                    //new MockQuestion { Id = 1, Content = "Question 1: 2 + 2 = ?", OptionA = "3", OptionB = "4", OptionC = "5", OptionD = "6", CorrectAnswer = "B" },
                    //new MockQuestion { Id = 2, Content = "Question 2: What is the shape of the Earth?", OptionA = "Round", OptionB = "Square", OptionC = "Triangle", OptionD = "Ellipse", CorrectAnswer = "A" }
                }
            }
        };

        private static List<MockExamAnswer> submittedAnswers = new();
        private static List<MockFeedback> feedbacks = new();

        // GET: /Exam/MockExam
        [HttpGet("MockExam")]
        public IActionResult MockExam()
        {
            return View("MyMockExams", mockExams);
        }

        // GET: /Exam/Take/1
        [HttpGet("Take/{id}")]
        public IActionResult Take(int id)
        {
            var exam = mockExams.FirstOrDefault(e => e.ExamId == id);
            if (exam == null) return NotFound();

            exam.Status = "In Progress";
            return View("DoExam", exam);
        }

        // POST: /Exam/Submit
        [HttpPost("Submit")]
        public IActionResult Submit(int examId, List<string> answers)
        {
            var exam = mockExams.FirstOrDefault(e => e.ExamId == examId);
            if (exam == null) return NotFound();

            var answerObj = new MockExamAnswer
            {
                MockExamAnswerID = submittedAnswers.Count + 1,
                MockExamId = examId,
                LecturerId = "user123",
                SubmittedAt = DateTime.Now,
                AnswerJson = System.Text.Json.JsonSerializer.Serialize(answers)
            };
            submittedAnswers.Add(answerObj);

            exam.Status = "Submitted";

            TempData["Message"] = "The mock exam has been submitted successfully.";
            return RedirectToAction("MockExam");
        }

        // GET: /Exam/Feedback/1
        [HttpGet("Feedback/{id}")]
        public IActionResult Feedback(int id)
        {
            var model = new MockFeedback
            {
                MockExamId = id
            };
            return View("Feedback", model);
        }

        // POST: /Exam/SaveFeedback
        [HttpPost("SaveFeedback")]
        public IActionResult SaveFeedback(MockFeedback feedback)
        {
            feedback.MockFeedbackID = feedbacks.Count + 1;
            feedback.FeedbackDate = DateTime.Now;
            feedback.LecturerId = "user123";

            feedbacks.Add(feedback);

            var exam = mockExams.FirstOrDefault(e => e.ExamId == feedback.MockExamId);
            if (exam != null) exam.Status = "Feedback Given";

            TempData["Message"] = "Feedback has been submitted.";
            return RedirectToAction("MockExam");
        }
    }
}
