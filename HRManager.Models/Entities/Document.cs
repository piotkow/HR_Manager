using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManager.Models.Entities
{
    [Table("Documents")]
    public class Document
    {
        [Key]
        public int DocumentID { get; set; }

        [Required, ForeignKey("Employees")]
        public int EmployeeID { get; set; }

        [Required, MaxLength(50)]
        public required string DocumentType { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required, MaxLength(500)]
        public required string Content { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
