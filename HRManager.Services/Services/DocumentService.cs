using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            return await _documentRepository.GetDocumentsAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            return await _documentRepository.GetDocumentByIdAsync(documentId);
        }

        public async Task InsertDocumentAsync(Document document)
        {
            await _documentRepository.InsertDocumentAsync(document);
        }

        public async Task DeleteDocumentAsync(int documentId)
        {
            await _documentRepository.DeleteDocumentAsync(documentId);
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            await _documentRepository.UpdateDocumentAsync(document);
        }
    }

}
