using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("CourseSchedules")]
    public class CourseSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int InstructorUserId { get; set; }

        [Required]
        public int SemesterId { get; set; }

        [Required, MaxLength(10)]
        public string SectionNumber { get; set; } = string.Empty;

        [Required]
        public int MaxStudents { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        [ForeignKey("InstructorUserId")]
        public virtual User? Instructor { get; set; }

        [ForeignKey("SemesterId")]
        public virtual Semester? Semester { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<FeedbackForm> FeedbackForms { get; set; } = new List<FeedbackForm>();
    }
}
