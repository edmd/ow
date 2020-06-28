
namespace orbital_witness.Domain.RulesEngine
{
    public class RulesEngine : IRulesEngine
    {
        public ProcessingAction RetrieveResponseAction(string responseCode, string responseMessage, Document document)
        {
            // Retrieve rules for third parties from repository (potential to be cached)
            // Evaluate incoming parameters as an expression against rule list
            // Determine ProcessingAction that needs to be applied to Document
            // Logging, set maxRetry, next processing date and Document 
            // specific actions as per Domain.DocumentStatus
            return ProcessingAction.Success;
        }

        public void HandleProcessingAction(ProcessingAction action, Document document)
        {
            // System notifications and templates, log user visible event comments, 
            // change document properties
        }
    }
}