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
        Task InsertDocumentAsync(Document document);
        Task DeleteDocumentAsync(int documentId);
        Task UpdateDocumentAsync(Document document);
    }



}
