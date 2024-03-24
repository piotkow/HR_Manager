using HRManager.Models.Entities;
using HRManager.Services.DTOs.PhotoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.EmployeeDTO
{
    public class EmployeePositionTeamResponse
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string PositionName { get; set; }
        public string PositionDescription { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public string Department { get; set; }
        public PhotoResponse Photo { get; set;}
    }
}
