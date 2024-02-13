using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
//using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.Services
{
    public interface IPatientService
	{
		Task<PatientProfile> GetPatientProfile(int patientId);
		Task<bool> UpdatePatientProfileAsync(PatientProfile patientProfile);
		Task<AccountSubscriptionInfo> GetPatientSubscriptionInfoAsync();
		Task<AccountAddFamilyMemberInfo> PatientGetAddFamilyMemberInfoAsync(int? numberOfFamilyMembers = null);
		//Task<StatusResponse> UploadPatientImageAsync(UpdateProfileImageParam param);
		Task<StatusResponse> DeactivateFamilyMemberAsync(int familyMemberPatientId);
		Task<StatusResponse> PatientMakeFamilyMemberPrivateAsync(int familyMemberPatientId, string email);
		Task<StatusResponse> ReactivateFamilyMemberInfoAsync(int familyMemberPatientId);
		Task<StatusResponse> ReactivateFamilyMemberAsync(int familyMemberPatientId);
		Task<StatusResponse> RemovePrivateFamilyMemberInfoAsync(int familyMemberPatientId);
		Task<StatusResponse> RemoveFamilyMemberAsync(int familyMemberPatientId, string email, bool newEmail);
		Task<StatusResponse> PatientGetCancelSubscriptionDateAsync();
		Task<StatusResponse> PatientCancelSubscriptionAsync();
		Task<SubscriptionChangeInfo> PatientGetChangeSubscriptionInfoAsync(int subscriptionOptionID, string promoCode = null);
		Task<List<BasicFamilyMemberInfo>> PatientGetFamilyMemberListAsync();
		Task<StatusResponse> PatientChangeSubscriptionAsync(AccountSubscriptionChange accountSubscriptionChange);
        Task<StatusResponse> ClickCancelPlanHyperlinkAsync();
        Task<StatusResponse> ClickCancelPlanButtonAsync();

        Task<StatusResponse> GetActiveCoverage(int familyMemberPatientId);

    }
}
