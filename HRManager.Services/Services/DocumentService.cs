using AutoMapper;
using Azure.Storage.Blobs;
using HRManager.Data.UnitOfWork;
using HRManager.Services.DTOs.DocumentDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using HRManager.Models.Entities;

namespace HRManager.Services.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, BlobServiceClient blobServiceClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;

        }

        public async Task<BlobClient> UploadDocumentAsync(IFormFile documentFile)
        {
            var containerInstance = _blobServiceClient.GetBlobContainerClient("documents");
            var blobInstance = containerInstance.GetBlobClient(documentFile.FileName);

            await blobInstance.UploadAsync(documentFile.OpenReadStream());
            return blobInstance;
        }

        public async Task<bool> DeleteDocumentAsync(int documentId, string blobUri, string Filename)
        {

            var containerInstance = _blobServiceClient.GetBlobContainerClient("documents");
            var blobName = Filename;
            var blobInstance = containerInstance.GetBlobClient(blobName);


            var document = await _unitOfWork.DocumentRepository.GetDocumentByIdAsync(documentId);
            bool ifExistInAzure = await blobInstance.ExistsAsync();

            if (document != null && ifExistInAzure)
            {
                await blobInstance.DeleteIfExistsAsync();
                await _unitOfWork.DocumentRepository.DeleteDocumentAsync(document.DocumentID);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
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
            document.IssueDate = documentReq.IssueDate;
            //document.Content = documentReq.Content;
            await _unitOfWork.DocumentRepository.UpdateDocumentAsync(document);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<DocumentEmployeeResponse>> GetDocumentsByEmployeeIdAsync(int employeeId)
        {
            var documents = await _unitOfWork.DocumentRepository.GetDocumentsByEmployeeIdAsync(employeeId);
            return _mapper.Map<IEnumerable<DocumentEmployeeResponse>>(documents);
        }
    }

}