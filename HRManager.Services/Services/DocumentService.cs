using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.DocumentDTO;
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
        private readonly IMapper _mapper;

        public DocumentService(IDocumentRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentEmployeeResponse>> GetDocumentsAsync()
        {
            var documents = await _documentRepository.GetDocumentsAsync();
            return _mapper.Map<IEnumerable<DocumentEmployeeResponse>>(documents);
        }

        public async Task<DocumentEmployeeResponse> GetDocumentByIdAsync(int documentId)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(documentId);
            return _mapper.Map<DocumentEmployeeResponse>(document);
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
