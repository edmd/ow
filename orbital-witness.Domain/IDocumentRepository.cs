using orbital_witness.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace orbital_witness.Domain
{
    public interface IDocumentRepository
    {
        Task<Document> GetDocument(string name);
        
        Task InsertDocument(Document document);

        Task<List<Document>> GetDocuments(DocumentStatus[] documentStatus);
    }
}