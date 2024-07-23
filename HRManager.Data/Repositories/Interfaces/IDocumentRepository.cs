using HRManager.Models.Entities;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IDocumentRepository : IDisposable
    {
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task<IEnumerable<Document>> GetDocumentsByEmployeeIdAsync(int employeeId);
        Task InsertDocumentAsync(Document document);
        Task DeleteDocumentAsync(int documentId);
        Task UpdateDocumentAsync(Document document);
        Task SaveAsync();
    }


}
