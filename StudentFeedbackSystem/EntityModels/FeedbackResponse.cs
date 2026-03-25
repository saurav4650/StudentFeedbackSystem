using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("FeedbackResponses")]
    public class FeedbackResponse
    {
        [Key]
        public int ResponseId { get; set; }

        [Required]
        public int FeedbackFormId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public int? RatingValue { get; set; }

        public string? TextValue { get; set; }

        [ForeignKey("FeedbackFormId")]
        public virtual FeedbackForm? FeedbackForm { get; set; }

        [ForeignKey("QuestionId")]
        public virtual FeedbackQuestion? Question { get; set; }
    }
}
