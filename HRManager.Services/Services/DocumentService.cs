using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentEmployeeResponse>> GetDocumentsAsync()
        {
            var documents = await _unitOfWork.DocumentRepository.GetDocumentsAsync();
            return _mapper.Map<IEnumerable<DocumentEmployeeResponse>>(documents);
        }

        public async Task<DocumentEmployeeResponse> GetDocumentByIdAsync(int documentId)
        {
            var document = await _unitOfWork.DocumentRepository.GetDocumentByIdAsync(documentId);
            return _mapper.Map<DocumentEmployeeResponse>(document);
        }

        public async Task<Document> InsertDocumentAsync(DocumentRequest documentReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var document = _mapper.Map<Document>(documentReq);
            await _unitOfWork.DocumentRepository.InsertDocumentAsync(document);
            await _unitOfWork.CommitAsync();
            return document;
        }

        public async Task DeleteDocumentAsync(int documentId)
        {
            await _unitOfWork.DocumentRepository.DeleteDocumentAsync(documentId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDocumentAsync(int documentId, DocumentRequest documentReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var document = await _unitOfWork.DocumentRepository.GetDocumentByIdAsync(documentId);
            document.EmployeeID = documentReq.EmployeeID;
            document.DocumentType = documentReq.DocumentType;
            document.IssueDate = documentReq.IssueDate;
            document.Content = documentReq.Content;
            await _unitOfWork.DocumentRepository.UpdateDocumentAsync(document);
            await _unitOfWork.CommitAsync();
        }
    }

}
