using HRManager.Models.Entities;
using HRManager.Services.DTOs.DocumentDTO;
using HRManager.Services.DTOs.PhotoDTO;
using HRManager.Services.Interfaces;
using HRManager.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IEnumerable<DocumentEmployeeResponse>> GetDocuments()
        {
            var documents = await _documentService.GetDocumentsAsync();
            return documents;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentEmployeeResponse>> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        [HttpGet("byEmployee/{employeeId}")]
        public async Task<IEnumerable<DocumentEmployeeResponse>> GetDocumentEmployeeId(int employeeId)
        {
            var documents = await _documentService.GetDocumentsByEmployeeIdAsync(employeeId);
            return documents;
        }

        [HttpPost("upload-document/{employeeId}")]
        public async Task<ActionResult> UploadDocument(IFormFile document, int employeeId)
        {
            if (document == null)
            {
                return BadRequest("No file provided.");
            }


            var allowedFileTypes = new[]
            {
                "image/jpeg", "image/jpg", "image/png", // Image types
                "application/pdf", // PDF
                "application/msword", // DOC
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document", // DOCX
                "text/plain", // TXT
                "application/vnd.ms-excel", // XLS
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" // XLSX
            };

            if (!allowedFileTypes.Contains(document.ContentType))
            {
                return BadRequest("Invalid file type. Only PDF, DOC, DOCX, TXT AND Image types(JPG,PNG) are allowed.");
            }

            var result = await _documentService.UploadDocumentAsync(document, employeeId);

            if (result != null) return Ok(result);
            return BadRequest("Problem adding file");
        }


        [HttpPost]
        public async Task<IActionResult> InsertDocument([FromBody] DocumentRequest documentReq)
        {
            var insertedDocument = await _documentService.InsertDocumentAsync(documentReq);
            return CreatedAtAction("GetDocumentById", new { id = insertedDocument.DocumentID }, insertedDocument);
        }

        [HttpDelete("{id}/{employeeId}")]
        public async Task<IActionResult> DeleteDocument(int id, int employeeId)
        {
            var documentToDelete = await _documentService.GetDocumentByIdAsync(id);
            await _documentService.DeleteDocumentAsync(id, documentToDelete.Uri, documentToDelete.Filename, employeeId);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentRequest documentReq)
        {
            await _documentService.UpdateDocumentAsync(id, documentReq);
            return Ok(documentReq);
        }


    }

}
