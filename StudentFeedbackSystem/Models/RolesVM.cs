using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class RolesVM
    {
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string RoleName { get; set; } = string.Empty;
    }
}
