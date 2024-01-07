using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HRManager.Models.Enums;

namespace HRManager.Models.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required, ForeignKey("Employees")]
        public required int EmployeeID { get; set; }

        [Required, MaxLength(20)]
        public required string Username { get; set; }

        [Required, MaxLength(100)]
        public required string Password { get; set; }

        [Required, MaxLength(20)]
        public required Role AccountType { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
