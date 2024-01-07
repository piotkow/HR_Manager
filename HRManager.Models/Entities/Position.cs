using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManager.Models.Entities
{
    [Table("Positions")]
    public class Position
    {
        [Key]
        public int PositionID { get; set; }

        [Required, MaxLength(50)]
        public required string PositionName { get; set; }

        [MaxLength(200)]
        public string? PositionDescription { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
