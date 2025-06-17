using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace QBCA.Models
{
    public class LecturerAssignment
    {
        public int Id { get; set; }
        public string TaskType { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public string AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LecturerName { get; set; }      
        public string ExamTitle { get; set; }          
        public DateTime Deadline { get; set; }     
        public bool IsReviewed { get; set; }
    }
}
