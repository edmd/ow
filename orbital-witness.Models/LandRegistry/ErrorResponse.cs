using Newtonsoft.Json;

namespace orbital_witness.Models
{
    public class ErrorResponse
    {
        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("detail")]
        public string detail { get; set; }

        [JsonProperty("instance")]
        public string instance { get; set; }

        [JsonProperty("additionalProp1")]
        public AdditionalProp1 additionalProp1 { get; set; }

        [JsonProperty("additionalProp2")]
        public AdditionalProp2 additionalProp2 { get; set; }

        [JsonProperty("additionalProp3")]
        public AdditionalProp3 additionalProp3 { get; set; }
    }

    public class AdditionalProp1
    {
    }

    public class AdditionalProp2
    {
    }

    public class AdditionalProp3
    {
    }
}