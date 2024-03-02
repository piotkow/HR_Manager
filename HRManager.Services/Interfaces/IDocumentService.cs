using HRManager.Models.Entities;
using HRManager.Services.DTOs.DocumentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentEmployeeResponse>> GetDocumentsAsync();
        Task<DocumentEmployeeResponse> GetDocumentByIdAsync(int documentId);
        Task<Document> InsertDocumentAsync(DocumentRequest documentReq);
        Task DeleteDocumentAsync(int documentId);
        Task UpdateDocumentAsync(int documentId, DocumentRequest documentReq);
    }



}
