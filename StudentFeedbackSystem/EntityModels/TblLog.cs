using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFeedbackSystem.EntityModels
{
    [Table("tblLog")]
    public class TblLog
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }

        [Required, MaxLength(100)]
        public string Controller { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Operation { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string LogLevel { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Message { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
