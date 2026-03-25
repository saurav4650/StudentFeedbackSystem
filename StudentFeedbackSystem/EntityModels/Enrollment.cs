using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("Enrollments")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required]
        public int StudentUserId { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        [MaxLength(5)]
        public string? Grade { get; set; }

        [ForeignKey("StudentUserId")]
        public virtual User? Student { get; set; }

        [ForeignKey("ScheduleId")]
        public virtual CourseSchedule? Schedule { get; set; }
    }
}
