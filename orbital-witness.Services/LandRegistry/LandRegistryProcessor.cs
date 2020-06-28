using orbital_witness.Domain;
using orbital_witness.Domain.RulesEngine;
using System.Threading.Tasks;

namespace orbital_witness.Services.LandRegistry
{
    public class LandRegistryProcessor
    {
        private ILandRegistryService _landRegistryService;
        private IRulesEngine _rulesEngine;

        public LandRegistryProcessor(ILandRegistryService landRegistryService, 
                                     IRulesEngine rulesEngine)
        {
            _landRegistryService = landRegistryService;
            _rulesEngine = rulesEngine;
        }

        public async Task<Document> StartDocumentRequest(Document document)
        {
            var landRegistryDoc = await _landRegistryService.StartDocumentRequest(document.Name);

            if(landRegistryDoc.HasErrors)
            {
                ProcessingAction processingAction = _rulesEngine.RetrieveResponseAction(
                    landRegistryDoc.ErrorResponse.status.ToString(), 
                    landRegistryDoc.ErrorResponse.detail,
                    document);
                _rulesEngine.HandleProcessingAction(processingAction, document);

                return document;
            } else
            {
                var documentResponse = document;
                documentResponse.ThirdPartyReference = landRegistryDoc.MessageId;

                return documentResponse;
            }
        }

        public async Task<Document> GetDocument(Document document)
        {
            var landRegistryDoc = await _landRegistryService.GetDocument(document.ThirdPartyReference);

            if (landRegistryDoc.HasErrors)
            {
                ProcessingAction processingAction = _rulesEngine.RetrieveResponseAction(
                    landRegistryDoc.ErrorResponse.status.ToString(),
                    landRegistryDoc.ErrorResponse.detail,
                    document);
                _rulesEngine.HandleProcessingAction(processingAction, document);

                return document;
            }
            else
            {
                var documentResponse = document;
                documentResponse.ThirdPartyReference = landRegistryDoc.MessageId;

                return documentResponse;
            }
        }

        public async Task<Document> GetDocumentStatus(Document document)
        {
            var landRegistryDoc = await _landRegistryService.GetStatus(document.ThirdPartyReference);
            var documentResponse = document;

            if (landRegistryDoc.HasErrors)
            {
                ProcessingAction processingAction = _rulesEngine.RetrieveResponseAction(
                    landRegistryDoc.ErrorResponse.status.ToString(),
                    landRegistryDoc.ErrorResponse.detail,
                    document);
                _rulesEngine.HandleProcessingAction(processingAction, document);

                documentResponse.Status = DocumentStatus.FailedThirdParty;
            }
            else
            {
                ProcessingAction processingAction = _rulesEngine.RetrieveResponseAction(
                    landRegistryDoc.Status.ToString(),
                    landRegistryDoc.Status.ToString(),
                    document);
                _rulesEngine.HandleProcessingAction(processingAction, document);

                if(landRegistryDoc.Status == Models.LandRegistryDocumentStatus.Created)
                {
                    documentResponse.Status = DocumentStatus.Processing;
                } else if(landRegistryDoc.Status == Models.LandRegistryDocumentStatus.NotFound)
                {
                    // If the same messageId could be resubmitted with a corrected name, we could change 
                    // the status of this document to "Failed" to allow it to be amended before resubmission
                    documentResponse.Status = DocumentStatus.Void; // Configured as an endstate
                }
            }

            return document;
        }
    }
}