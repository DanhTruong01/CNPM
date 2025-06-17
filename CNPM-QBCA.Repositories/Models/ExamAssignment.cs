using System;
using System.ComponentModel.DataAnnotations;

namespace QBCA.Models
{
    public class ExamAssignment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lecturer Name is required.")]
        public string LecturerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Exam Title is required.")]
        public string ExamTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Deadline is required.")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public string Comments { get; set; } = string.Empty;

        public bool IsReviewed { get; set; } = false;
    }
}
