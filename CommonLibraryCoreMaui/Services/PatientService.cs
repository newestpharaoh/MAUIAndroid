using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Exceptions;
using CommonLibraryCoreMaui.Models;
//using CommonLibraryCoreMaui.ViewModels;

namespace CommonLibraryCoreMaui.Services
{
	public class PatientService: IPatientService
	{
		public async Task<PatientProfile> GetPatientProfile(int patientId)
		{
			return await DataUtility.GetPatientProfileAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, patientId == 0 ? (int)Globals.Instance.UserInfo.PatientID : patientId).ConfigureAwait(false);
		}

		public async Task<bool> UpdatePatientProfileAsync(PatientProfile patientProfile)
		{
			StatusResponse resp = await DataUtility.UpdatePatientProfileAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, patientProfile).ConfigureAwait(false);
			if (resp.StatusCode != StatusCode.Success)
				throw new PatientException(resp.Message);

			return resp.StatusCode == StatusCode.Success;
		}

		public async Task<AccountSubscriptionInfo> GetPatientSubscriptionInfoAsync()
		{
			return await DataUtility.PatientGetSubscriptionInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<AccountAddFamilyMemberInfo> PatientGetAddFamilyMemberInfoAsync(int? numberOfFamilyMembers = null)
		{
			return await DataUtility.PatientGetAddFamilyMemberInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token, numberOfFamilyMembers).ConfigureAwait(false);
		}

		//public async Task<StatusResponse> UploadPatientImageAsync(UpdateProfileImageParam param)
		//{
  //          StatusResponse resp = await DataUtility.UploadPatientImageAsync(SettingsValues.ApiURLValue, param.ProfileId == 0 ? (int)Globals.Instance.UserInfo.PatientID : param.ProfileId, CommonAuthSession.Token, param.ProfileImage).ConfigureAwait(false);
		//	if (resp.StatusCode != StatusCode.SuccessSeePayload)
		//		throw new ProviderException(resp.Message);

		//	return resp;
		//}

		public async Task<StatusResponse> DeactivateFamilyMemberAsync(int familyMemberPatientId)
		{
			return await DataUtility.DeactivateFamilyMemberAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, familyMemberPatientId, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> PatientMakeFamilyMemberPrivateAsync(int familyMemberPatientId, string email)
		{
			return await DataUtility.PatientMakeFamilyMemberPrivateAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, familyMemberPatientId, email, CommonAuthSession.Token);
		}
		
		public async Task<StatusResponse> ReactivateFamilyMemberAsync(int familyMemberPatientId)
		{
			return await DataUtility.ReactivateFamilyMemberAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, familyMemberPatientId, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> ReactivateFamilyMemberInfoAsync(int familyMemberPatientId)
		{
			return await DataUtility.ReactivateFamilyMemberInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, familyMemberPatientId, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> RemovePrivateFamilyMemberInfoAsync(int familyMemberPatientId)
		{
			return await DataUtility.RemovePrivateFamilyMemberInfoAsync(SettingsValues.ApiURLValue, familyMemberPatientId, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> RemoveFamilyMemberAsync(int familyMemberPatientId, string email, bool newEmail)
		{
			return await DataUtility.RemoveFamilyMemberAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, familyMemberPatientId, email, newEmail, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<SubscriptionChangeInfo> PatientGetChangeSubscriptionInfoAsync(int subscriptionOptionID, string promoCode=null)
		{
			return await DataUtility.PatientGetChangeSubscriptionInfoAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, subscriptionOptionID, CommonAuthSession.Token,promoCode).ConfigureAwait(true);
		}

		public async Task<List<BasicFamilyMemberInfo>> PatientGetFamilyMemberListAsync()
		{
			return await DataUtility.PatientGetFamilyMemberListAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> PatientChangeSubscriptionAsync(AccountSubscriptionChange accountSubscriptionChange)
		{
			return await DataUtility.PatientChangeSubscriptionAsync(SettingsValues.ApiURLValue, accountSubscriptionChange, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> PatientGetCancelSubscriptionDateAsync()
		{
			return await DataUtility.PatientGetCancelSubscriptionDateAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> PatientCancelSubscriptionAsync()
		{
			return await DataUtility.PatientCancelSubscriptionAsync(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token).ConfigureAwait(false);
		}

		public async Task<StatusResponse> ClickCancelPlanHyperlinkAsync()
		{
			return await DataUtility.ClickCancelPlanHyperlink(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token, "en").ConfigureAwait(false);
		}

        //ClickCancelPlanButton
        public async Task<StatusResponse> ClickCancelPlanButtonAsync()
        {
            return await DataUtility.ClickCancelPlanButton(SettingsValues.ApiURLValue, Globals.Instance.UserInfo.PatientID, CommonAuthSession.Token, "en").ConfigureAwait(false);
        }

        public async Task<StatusResponse> GetActiveCoverage(int familyMemberPatientId)
        {
            return await DataUtility.GetActiveCoverage(SettingsValues.ApiURLValue, familyMemberPatientId, CommonAuthSession.Token, "en").ConfigureAwait(false);
        }
    }
}
