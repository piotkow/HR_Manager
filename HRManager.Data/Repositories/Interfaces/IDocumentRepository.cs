using HRManager.Models.Entities;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IDocumentRepository : IDisposable
    {
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task InsertDocumentAsync(Document document);
        Task DeleteDocumentAsync(int documentId);
        Task UpdateDocumentAsync(Document document);
        Task SaveAsync();
    }


}
