using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Models.Entities
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }
        public string Filename { get; set; }
        public string Uri { get; set; }
        [ForeignKey("EmployeeID")]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
