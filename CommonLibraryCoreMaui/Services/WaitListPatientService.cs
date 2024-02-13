using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public class WaitListPatientService : IWaitListPatientService
    {
        public WaitListPatientService()
        {
        }

        public async Task<ProviderWaitListResponse> GetWaitList(int providerId)
        {
            return await DataUtility.GetProviderWaitListAsync(SettingsValues.ApiURLValue, providerId.ToString(), CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<ProviderStartVisitResponse> ProviderStartVisit(int visitId, int providerId)
        {
            return await DataUtility.ProviderStartVisitAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, visitId, providerId).ConfigureAwait(false);
        }
    }
}