using HRManager.Models.Entities;
using HRManager.Services.DTOs.DocumentDTO;
using HRManager.Services.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> InsertDocument([FromBody] DocumentRequest documentReq)
        {
            var insertedDocument = await _documentService.InsertDocumentAsync(documentReq);
            return CreatedAtAction("GetDocumentById", new { id = insertedDocument.DocumentID }, insertedDocument);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _documentService.DeleteDocumentAsync(id);
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
