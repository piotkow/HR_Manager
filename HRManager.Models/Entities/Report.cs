using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HRManager.Models.Enums;

namespace HRManager.Models.Entities
{
    [Table("Reports")]
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        [Required, MaxLength(100)]
        public required string Title { get; set; }

        [Required, MaxLength(1000)]
        public required string Content { get; set; }

        [Required, MaxLength(50)]
        public required Severity Severity { get; set; }

        [MaxLength(200)]
        public required bool Result { get; set; }

        [Required, ForeignKey("Author")]
        public required int AuthorID { get; set; }

        [Required, ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Employee Author { get; set; }
        public Employee Employee { get; set; }
    }
}
