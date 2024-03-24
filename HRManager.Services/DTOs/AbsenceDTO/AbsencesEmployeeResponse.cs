using HRManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.AbsenceDTO
{
    public class AbsencesEmployeeResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public string RejectionReason { get; set; }
    }
}
