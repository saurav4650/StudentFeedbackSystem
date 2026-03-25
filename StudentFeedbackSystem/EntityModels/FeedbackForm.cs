using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("FeedbackForms")]
    public class FeedbackForm
    {
        [Key]
        public int FeedbackFormId { get; set; }

        [Required]
        public int StudentUserId { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public int? OverallRating { get; set; }

        [ForeignKey("StudentUserId")]
        public virtual User? Student { get; set; }

        [ForeignKey("ScheduleId")]
        public virtual CourseSchedule? Schedule { get; set; }

        public virtual ICollection<FeedbackResponse> FeedbackResponses { get; set; } = new List<FeedbackResponse>();
    }
}
