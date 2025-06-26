using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QBCA.Models;

namespace QBCA.ViewModels
{
    public class SubmissionApprovalViewModel
    {
        public int SubmissionApprovalID { get; set; }

        [Required]
        public int SubmissionTableID { get; set; }

        [Required]
        public int ApprovedByID { get; set; }

        public string SubmissionTitle { get; set; }
        public string SubjectName { get; set; }
        public string Status { get; set; }
        public string ApprovedByName { get; set; }

        [Display(Name = "Approved Date")]
        public DateTime ApprovedDate { get; set; }
        public string Feedback { get; set; }


        public List<SubmissionTable> AllSubmissions { get; set; } = new();
        public List<User> AllApprovers { get; set; } = new();
    }
}
