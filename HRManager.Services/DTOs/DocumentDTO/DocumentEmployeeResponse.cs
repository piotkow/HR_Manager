using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.DocumentDTO
{
    public class DocumentEmployeeResponse
    {
        public int DocumentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime IssueDate { get; set; }
        public string Filename { get; set; }
        public string Uri { get; set; }

    }
}
