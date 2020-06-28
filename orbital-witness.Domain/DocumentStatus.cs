
namespace orbital_witness.Domain
{
    /// <summary>
    /// This enum is just the tip. It illustrates the concept that the systems' processes MUST be decoupled.
    /// - The Landregistry is not the only API of its type; there will be many.
    /// - It's vital our statuses are granular enough to share this information with the user so that they 
    ///     know where in the process their request is.
    /// - Different document statuses from different Third Parties will need to be processed and mapped differently, 
    ///     this will also change over time. A mature solution is a Rules Engine that can handle incoming 
    ///     document status changes (without needing to redeploy if we change those mappings). The incoming documents 
    ///     can then be placed in the correct queue(s) per their status i.e. initiate OCR; send to big data; 
    ///     calculate risk profile; send a 'ready' email notification. Most importantly you can update the 
    ///     process_date on document record to future dates (i.e. configured to ~10min), to automate 
    ///     polling for long running Third Party tasks.
    /// </summary>
    public enum DocumentStatus
    {
        Cancelled,
        CancellingThirdParty,
        CancelledThirdParty,
        Completed,
        Created, // Useful initial hold state
        CreatedThirdParty, // Created
        Failed, // NotFound
        FailedThirdParty,
        Locked, 
        Processing,
        ProcessingThirdParty, // NotReady
        Void,
        VoidThirdParty
    }
}