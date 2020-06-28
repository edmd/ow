using orbital_witness.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using orbital_witness.Domain.RulesEngine;

namespace orbital_witness
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Dynamically load up config settings
            var username = "test";
            var url = "https://ow-interview-exercise-dev.azurewebsites.net";

            var landRegistryService = new LandRegistryService(
                new LoggerFactory().CreateLogger<LandRegistryService>(),
                new RulesEngine(),
                new LandRegistryClient(new HttpClient(), username).Client,
                url);
        }
    }
}