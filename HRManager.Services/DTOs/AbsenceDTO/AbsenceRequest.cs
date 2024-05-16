using HRManager.Models.Entities;
using HRManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.AbsenceDTO
{
    public class AbsenceRequest
    {
        public int EmployeeID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required Status Status { get; set; }
        public string? RejectionReason { get; set; }
    }
}
