using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class CoursesVM
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Course Code is required")]
        public string CourseCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string CourseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Credits is required")]
        [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10")]
        public int Credits { get; set; }

        public string? DepartmentName { get; set; }
    }
}
