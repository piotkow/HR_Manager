using HRManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTO
{
    public class EmployeeReportResponse
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Severity Severity { get; set; }
        public string Result { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
    }
}
