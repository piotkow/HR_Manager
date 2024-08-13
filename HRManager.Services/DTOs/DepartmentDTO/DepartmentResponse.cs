using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.DepartmentDTO
{
    public class DepartmentResponse
    {
        public int DerpartmentID { get; set; }

        public string Name { get; set; }
    }
}
