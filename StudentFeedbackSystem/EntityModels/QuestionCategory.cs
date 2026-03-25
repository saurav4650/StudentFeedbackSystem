using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("QuestionCategories")]
    public class QuestionCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public int DisplayOrder { get; set; }

        public virtual ICollection<FeedbackQuestion> FeedbackQuestions { get; set; } = new List<FeedbackQuestion>();
    }
}
