using System;
using System.ComponentModel.DataAnnotations;

namespace QBCA.Models
{
    public class ExamApproveTask
    {
        public int Id { get; set; }
        public string ExamTitle { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public string Reviewer { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        public DateTime DueDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }

}
