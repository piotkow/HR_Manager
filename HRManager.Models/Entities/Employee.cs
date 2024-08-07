﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HRManager.Models.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, Phone]
        public required string Phone { get; set; }

        [Required, MaxLength(30)]
        public string Country { get; set; }
        [Required, MaxLength(30)]
        public string City { get; set; }
        [Required, MaxLength(30)]
        public string Street { get; set; }
        [Required, MaxLength(30)]
        public string PostalCode { get; set; }

        [Required]
        public DateTime DateOfEmployment { get; set; }

        [Required, ForeignKey("Positions")]
        public required int PositionID { get; set; }

        [ForeignKey("Teams"), AllowNull]
        public int? TeamID { get; set; }

        public virtual Position Position { get; set; }
        public virtual Team Team { get; set; }

        public virtual Account Account { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Absence> Absences { get; set; }
        [InverseProperty("Author")]
        public virtual ICollection<Report> AuthoredReports { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Report> ReportsAboutEmployee { get; set; }
    }
}
