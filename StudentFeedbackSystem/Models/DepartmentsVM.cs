using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class DepartmentsVM
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Code is required")]
        public string DepartmentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
