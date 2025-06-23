using Microsoft.AspNetCore.Mvc;

namespace CNPM_QBCA.Models
{
    public class MockExam
    {
        public int MockExamID { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; } // Pending, In Progress, Submitted, Feedback Given
        public ICollection<MockQuestion> Questions { get; set; }
        public virtual ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public virtual ICollection<MockExamAnswer> Answers { get; set; } = new List<MockExamAnswer>();
        public virtual ICollection<MockFeedback> Feedbacks { get; set; } = new List<MockFeedback>();


    }

}
