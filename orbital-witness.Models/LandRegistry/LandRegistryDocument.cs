using Newtonsoft.Json;

namespace orbital_witness.Models
{
    public class LandRegistryDocument
    {
        /// <summary>
        /// The requirements makes no mention that the land registry documents 
        /// are immutable. For this exercise we will treat each request atomically.
        /// If they were indeed immutable documents we could look to homogenize the request
        /// by removing any unique constraint on messageId and searching the Repo by property Name, 
        /// The T&C's with the client would need to enforce that the contract is with us and not the 
        /// Registry to allow for this.
        /// </summary>
        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public LandRegistryDocumentStatus Status { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("additionalInfo", NullValueHandling = NullValueHandling.Ignore)]
        public AdditionalInfo AdditionalInfo { get; set; }

        [JsonProperty("errorResponse", NullValueHandling = NullValueHandling.Ignore)]

        [JsonIgnore]
        public bool HasErrors { get; set; } 

        public ErrorResponse ErrorResponse { get; set; }
    }

    public class AdditionalInfo
    {

        [JsonProperty("additionalProp1", NullValueHandling = NullValueHandling.Ignore)]
        public string AdditionalProp1 { get; set; }

        [JsonProperty("additionalProp2", NullValueHandling = NullValueHandling.Ignore)]
        public string AdditionalProp2 { get; set; }

        [JsonProperty("additionalProp3", NullValueHandling = NullValueHandling.Ignore)]
        public string AdditionalProp3 { get; set; }
    }
}