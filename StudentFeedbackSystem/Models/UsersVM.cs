using System.ComponentModel.DataAnnotations;

namespace StudentFeedbackSystem.Models
{
    public class UsersVM
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a role")]
        public int RoleId { get; set; }

        public int? DepartmentId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150, ErrorMessage = "Max 150 characters")]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "User Number is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string UserNumber { get; set; } = string.Empty;

        public string? RoleName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
