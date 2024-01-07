using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task InsertDocumentAsync(Document document);
        Task DeleteDocumentAsync(int documentId);
        Task UpdateDocumentAsync(Document document);
    }

}
