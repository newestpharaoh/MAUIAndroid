using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public interface IWaitListPatientService
    {
        Task<ProviderWaitListResponse> GetWaitList(int providerId);
        Task<ProviderStartVisitResponse> ProviderStartVisit(int visitId, int providerId);
    }
}