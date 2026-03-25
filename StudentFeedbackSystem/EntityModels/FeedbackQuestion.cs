using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("FeedbackQuestions")]
    public class FeedbackQuestion
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, MaxLength(500)]
        public string QuestionText { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string QuestionType { get; set; } = string.Empty;

        [Required]
        public int DisplayOrder { get; set; }

        [ForeignKey("CategoryId")]
        public virtual QuestionCategory? Category { get; set; }

        public virtual ICollection<FeedbackResponse> FeedbackResponses { get; set; } = new List<FeedbackResponse>();
    }
}
