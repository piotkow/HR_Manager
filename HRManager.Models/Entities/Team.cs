using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.Models.Entities
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Required, MaxLength(50)]
        public required string TeamName { get; set; }

        [ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; }

        [MaxLength(200)]
        public required string TeamDescription { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual Department Department { get; set; }
    }
}
