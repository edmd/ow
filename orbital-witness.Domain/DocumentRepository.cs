using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orbital_witness.Domain
{
    public class DocumentRepository : IDocumentRepository
    {
        public async Task<Document> GetDocument(string name)
        {
            return await Task.FromResult(new Document());
        }

        public async Task InsertDocument(Document document)
        {
            // Insert or update
            return;
        }

        public async Task<List<Document>> GetDocuments(DocumentStatus[] documentStatuses)
        {
            return await Task.FromResult(new List<Document>().Where(x => documentStatuses.Contains(x.Status) ).ToList());
        }
    }
}