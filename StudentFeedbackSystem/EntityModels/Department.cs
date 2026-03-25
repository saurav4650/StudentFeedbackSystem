using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required, MaxLength(10)]
        public string DepartmentCode { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
