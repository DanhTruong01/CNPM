namespace QBCA.Models
{
    public class ReviewExam
    {
        public int Id { get; set; }
        public string ExamTitle { get; set; }
        public string SubjectName { get; set; }
        public string LecturerName { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string Status { get; set; }
    }
}
