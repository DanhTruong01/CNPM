using System;
using System.ComponentModel.DataAnnotations;

namespace QBCA.Models
{
    public class SubmissionApproval
    {
        [Key]
        public int SubmissionApprovalID { get; set; }

        [Required]
        public int SubmissionTableID { get; set; }
        public SubmissionTable? SubmissionTable { get; set; }

        [Required]
        public int ApprovedBy { get; set; }
        public User? Approver { get; set; }

        public DateTime ApprovedDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pending";
        public string? Feedback { get; set; }
    }
}
