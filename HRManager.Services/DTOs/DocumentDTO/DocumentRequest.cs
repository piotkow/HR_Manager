using HRManager.Models.Entities;

namespace HRManager.Services.DTOs.DocumentDTO
{
    public class DocumentRequest
    {
        public int EmployeeID { get; set; }
        public required string DocumentType { get; set; }
        public DateTime IssueDate { get; set; }
        public required byte[] Content { get; set; }
    }
}
