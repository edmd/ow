using System.Net.Http;

namespace orbital_witness.Services
{
    public class LandRegistryClient
    {
        public const string usernameHeader = "username";
        public HttpClient Client { get; }

        public LandRegistryClient(HttpClient httpClient, string username)
        {
            Client = httpClient;
            Client.DefaultRequestHeaders.Add(usernameHeader, username);
        }
    }
}