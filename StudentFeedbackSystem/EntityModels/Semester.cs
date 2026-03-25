using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("Semesters")]
    public class Semester
    {
        [Key]
        public int SemesterId { get; set; }

        [Required, MaxLength(50)]
        public string SemesterName { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public virtual ICollection<CourseSchedule> CourseSchedules { get; set; } = new List<CourseSchedule>();
    }
}
