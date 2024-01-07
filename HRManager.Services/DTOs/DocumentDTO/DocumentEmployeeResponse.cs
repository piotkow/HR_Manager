using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.DocumentDTO
{
    public class DocumentEmployeeResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public DateTime IssueDate { get; set; }
        public byte[] Content { get; set; }
    }
}
