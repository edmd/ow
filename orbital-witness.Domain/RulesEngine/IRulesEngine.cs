using System;
using System.Collections.Generic;
using System.Text;

namespace orbital_witness.Domain.RulesEngine
{
    public interface IRulesEngine
    {
        public ProcessingAction RetrieveResponseAction(string responseCode, string responseMessage, Document document);

        public void HandleProcessingAction(ProcessingAction action, Document document);
    }
}