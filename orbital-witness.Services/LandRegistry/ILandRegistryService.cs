using orbital_witness.Domain;
using orbital_witness.Models;
using System.Threading.Tasks;

namespace orbital_witness.Services
{
    public interface ILandRegistryService
    {
        Task<LandRegistryDocument> StartDocumentRequest(string name);

        Task<LandRegistryDocument> GetDocument(string thirdPartyReference);

        Task<LandRegistryDocument> GetStatus(string thirdPartyReference);
    }
}