using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HRManager.Models.Enums;



namespace HRManager.Models.Entities
{
    [Table("Absences")]
    public class Absence
    {
        [Key]
        public int AbsenceID { get; set; }

        [Required, ForeignKey("Employees")]
        public int EmployeeID { get; set; }

        public string Description {  get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, MaxLength(50)]
        public required Status Status { get; set; }

        [MaxLength(200)]
        public string? RejectionReason { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
