using orbital_witness.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using orbital_witness.Domain.RulesEngine;
using System.Net;

namespace orbital_witness.Services
{
    /// <summary>
    /// These 3 methods exposed on the Mock Land Registry Api indicate its Stateful 
    /// (i.e. an internal token needs to be generated and passed to the API to track 
    /// the progress in subsequent requests).
    /// 
    /// The service doesn't allow for a callback mechanism (aka webhooks) to be supplied 
    /// by the calling client, this would improve response times and reduce unnecessary 
    /// status update traffic on their API. For now we will revert to polling.
    /// 
    /// Polling needs to be configured long enough into the future to catch the Status change event.
    /// The polling frequency and duration can be configured with the RulesEngine
    /// </summary>
    public class LandRegistryService : ILandRegistryService
    {
        private readonly string _domain;
        private readonly HttpClient _httpClient;
        private readonly ILogger<LandRegistryService> _logger;

        public LandRegistryService(ILogger<LandRegistryService> logger, 
                                   HttpClient httpClient, 
                                   string domain)
        {
            _logger = logger;
            _httpClient = httpClient;
            _domain = domain;
        }

        /// <summary>
        /// Creates the initial document reference with the third party by using the name
        /// </summary>
        public async Task<LandRegistryDocument> StartDocumentRequest(string name)
        {
            try
            {
                var landRegistryDoc = new LandRegistryDocument
                {
                    MessageId = name
                };
                var jsonString = JsonConvert.SerializeObject(landRegistryDoc);
                var content = new StringContent(jsonString);

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_domain}/Documents"),
                    Method = HttpMethod.Post,
                    Content = content
                };

                var response = await _httpClient.SendAsync(request);
                var responseString = response.Content.ReadAsStringAsync().Result;

                switch(response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                        landRegistryDoc.HasErrors = true;
                        landRegistryDoc.ErrorResponse = errorResponse;
                        break;
                    case HttpStatusCode.Accepted:
                        landRegistryDoc = JsonConvert.DeserializeObject<LandRegistryDocument>(responseString);
                        break;
                }

                return landRegistryDoc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<LandRegistryDocument> GetDocument(string thirdPartyReference)
        {
            try
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_domain}/Documents/{thirdPartyReference}"),
                    Method = HttpMethod.Get
                };

                var response = await _httpClient.SendAsync(request);
                var responseString = response.Content.ReadAsStringAsync().Result;
                var landRegistryDoc = (LandRegistryDocument)null;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        landRegistryDoc = JsonConvert.DeserializeObject<LandRegistryDocument>(responseString);
                        break;
                    default:
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                        landRegistryDoc.HasErrors = true;
                        landRegistryDoc.ErrorResponse = errorResponse;
                        break;
                }

                return landRegistryDoc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<LandRegistryDocument> GetStatus(string thirdPartyReference)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Documents​/{thirdPartyReference}​/Status");
                var responseString = response.Content.ReadAsStringAsync().Result;
                var landRegistryDoc = (LandRegistryDocument)null;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                        landRegistryDoc.HasErrors = true;
                        landRegistryDoc.ErrorResponse = errorResponse;
                        break;
                    default:
                        landRegistryDoc = JsonConvert.DeserializeObject<LandRegistryDocument>(responseString);
                        break;
                }

                return landRegistryDoc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
    }
}