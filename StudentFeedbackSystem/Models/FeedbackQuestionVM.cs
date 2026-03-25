using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class FeedbackQuestionVM
    {
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Question Text is required")]
        [StringLength(500, ErrorMessage = "Max 500 characters")]
        public string QuestionText { get; set; } = string.Empty;

        [Required(ErrorMessage = "Question Type is required")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string QuestionType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Display Order is required")]
        [Range(1, 100, ErrorMessage = "Must be between 1 and 100")]
        public int DisplayOrder { get; set; }

        public string? CategoryName { get; set; }
    }
}
