using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class SemestersVM
    {
        public int SemesterId { get; set; }

        [Required(ErrorMessage = "Semester Name is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string SemesterName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
    }
}
