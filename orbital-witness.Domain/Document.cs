using System;
using System.Collections.Generic;
using System.Text;

namespace orbital_witness.Domain
{
    /// <summary>
    /// Internal representation
    /// </summary>
    public class Document
    {
        public Document()
        {
        }

        /// <summary>
        /// OW Document reference
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the document
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The third-party's reference to the document
        /// </summary>
        public string ThirdPartyReference { get; set; }

        /// <summary>
        /// The ApiId is the Id of the associated Api settings that is associated with the document
        /// </summary>
        public int ApiId { get; set; }

        /// <summary>
        /// Our status
        /// </summary>
        public DocumentStatus Status { get; set; }

        /// <summary>
        /// The time of the next processing date
        /// </summary>
        public DateTime ProcessDate { get; set; }

        /// <summary>
        /// Retrieved from Rules and decremented upon processing
        /// </summary>
        public int MaxRetries { get; set; }

        /// <summary>
        /// We lock a record when it's being processed
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// An example of all the other fields of the document
        /// </summary>
        public Object PDF { get; set; }
    }
}