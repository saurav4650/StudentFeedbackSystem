using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class QuestionCategoryVM
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Display Order is required")]
        [Range(1, 100, ErrorMessage = "Must be between 1 and 100")]
        public int DisplayOrder { get; set; }
    }
}
