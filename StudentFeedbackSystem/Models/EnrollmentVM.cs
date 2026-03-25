using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class EnrollmentVM
    {
        public int EnrollmentId { get; set; }

        [Required(ErrorMessage = "Student is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a student")]
        public int StudentUserId { get; set; }

        [Required(ErrorMessage = "Schedule is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a schedule")]
        public int ScheduleId { get; set; }

        [StringLength(10, ErrorMessage = "Max 10 characters")]
        public string? Grade { get; set; }

        public string? Student { get; set; }
        public string? Course { get; set; }
        public string? Instructor { get; set; }
        public string? Semester { get; set; }
        public string? SectionNumber { get; set; }
    }
}
