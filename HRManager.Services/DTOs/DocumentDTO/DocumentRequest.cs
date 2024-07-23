using HRManager.Models.Entities;

namespace HRManager.Services.DTOs.DocumentDTO
{
    public class DocumentRequest
    {
        public int EmployeeID { get; set; }
        public string Filename { get; set; }
        public DateTime IssueDate { get; set; }
        public string Uri { get; set; }
    }
}
