
namespace orbital_witness.Domain.RulesEngine
{
    /// <summary>
    ///  The pattern is you're adding action types, these can be for any domain model
    /// </summary>
    public enum ProcessingAction
    {
        Cancel = 1,
        Fail = 2,
        GetStatus = 4,
        Retry = 8, // Retry last document action
        Success = 16
    }
}