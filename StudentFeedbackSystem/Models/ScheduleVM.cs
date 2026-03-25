using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class ScheduleVM
    {
        public int ScheduleId { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a course")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Instructor is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an instructor")]
        public int InstructorUserId { get; set; }

        [Required(ErrorMessage = "Semester is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a semester")]
        public int SemesterId { get; set; }

        [Required(ErrorMessage = "Section Number is required")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string SectionNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Max Students is required")]
        [Range(1, 500, ErrorMessage = "Must be between 1 and 500")]
        public int MaxStudents { get; set; }

        public string? CourseName { get; set; }
        public string? Instructor { get; set; }
        public string? Semester { get; set; }
    }
}
