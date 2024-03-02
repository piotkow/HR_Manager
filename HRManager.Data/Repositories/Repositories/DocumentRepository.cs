using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace HRManager.Data.Repositories.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly HRManagerDbContext context;

        public DocumentRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            return await context.Documents.Include(e=>e.Employee).ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            return await context.Documents.Include(d=>d.Employee).FirstOrDefaultAsync(d=>d.DocumentID==documentId);
        }

        public async Task InsertDocumentAsync(Document document)
        {
            await context.Documents.AddAsync(document);
        }

        public async Task DeleteDocumentAsync(int documentId)
        {
            Document document = await context.Documents.FindAsync(documentId);
            if (document != null)
            {
                context.Documents.Remove(document);
            }
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            context.Documents.Update(document);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
