using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.TeamDTO
{
    public class TeamDepartmentResponse
    {
        public int TeamID { get; set; }
        public required string TeamName { get; set; }
        public required string TeamDescription { get; set; }
        public string DepartmentName { get; set; }
    }
}
