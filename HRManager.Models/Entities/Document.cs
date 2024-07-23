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

        [Required]
        public DateTime IssueDate { get; set; }

        public string Filename { get; set; }

        public string Uri { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
