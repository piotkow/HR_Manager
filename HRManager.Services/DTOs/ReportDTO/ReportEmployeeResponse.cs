using HRManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.ReportDTO
{
    public class ReportEmployeeResponse
    {
        public string Title { get; set; }
        public byte[] Content { get; set; }
        public Severity Severity { get; set; }
        public bool Result { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }
}
