using HRManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.ReportDTO
{
    public class ReportRequest
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required Severity Severity { get; set; }
        public required bool Result { get; set; }
        public required int AuthorID { get; set; }
        public int EmployeeID { get; set; }
    }
}
