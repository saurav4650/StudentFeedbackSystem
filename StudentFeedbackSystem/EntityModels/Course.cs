using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required, MaxLength(20)]
        public string CourseCode { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public int Credits { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; } = new List<CourseSchedule>();
    }
}
