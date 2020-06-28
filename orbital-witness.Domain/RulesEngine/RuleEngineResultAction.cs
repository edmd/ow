
namespace orbital_witness.Domain.RulesEngine
{
    public class RuleEngineResultAction
    {
        public ProcessingAction Action { get; set; }
        public int? MaxRetryAttempts { get; set; }
        public bool MaxRetryAttemptsReached { get; set; }
        public int? RetryDelay { get; set; }
    }
}