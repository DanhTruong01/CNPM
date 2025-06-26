using System.ComponentModel.DataAnnotations;

namespace CNPM_QBCA.Models
{
    public class MockQuestion
    {
        [Key]
        public int QuestionID { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;

        public string CorrectAnswer { get; set; } = string.Empty;
    }
}
