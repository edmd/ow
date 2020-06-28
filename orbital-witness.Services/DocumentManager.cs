using orbital_witness.Domain;
using orbital_witness.Services.LandRegistry;
using System.Threading;
using System.Threading.Tasks;

namespace orbital_witness.Services
{
    /// <summary>
    /// DocumentManager orchestrate Documents between the repository and Third Party providers
    /// </summary>
    public class DocumentManager
    {
        private IDocumentRepository _documentRepository;
        private LandRegistryProcessor _landRegistryProcessor;
        //private Timer _timer; // Azure function, consumer in pub/sub, timer, queue

        public DocumentManager(IDocumentRepository documentRepository, 
                               LandRegistryProcessor landRegistryProcessor)
        {
            _documentRepository = documentRepository;
            _landRegistryProcessor = landRegistryProcessor;
            //_timer = new Timer();
        }

        /// <summary>
        /// TODO: The loop needs to be thread-pooled
        /// </summary>
        public async void StartProcess()
        {
            var documentStatuses = new DocumentStatus[] { DocumentStatus.CreatedThirdParty };

            var documents = await _documentRepository.GetDocuments(documentStatuses);

            foreach(var document in documents)
            {
                var doc = await _landRegistryProcessor.GetDocumentStatus(document);

                if(doc.Status == DocumentStatus.Created)
                {
                    doc = await _landRegistryProcessor.GetDocument(document);
                }

                await _documentRepository.InsertDocument(doc);
            }
        }

        public async Task<Document> InitiateDocumentRetrieval(string name)
        {
            // if name is not null
            //     check if the repo has an existing document under that name
            //     retrieve the document from the landregistry if document not in CreatedThirdParty status
            // else if document is in CreatedThirdParty status
            //     simply return it
            var document = await _documentRepository.GetDocument(name);
            var returnedDoc = (Document)null;

            if(document != null && document.Status == DocumentStatus.Created)
            {
                returnedDoc = await _landRegistryProcessor.StartDocumentRequest(document);
            } else
            {
                document = new Document
                {
                    Name = name
                };

                returnedDoc = await _landRegistryProcessor.StartDocumentRequest(document);
            }

            await _documentRepository.InsertDocument(returnedDoc);

            return returnedDoc;
        }
    }
}