using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Models.Entities
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int DerpartmentID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

    }
}
