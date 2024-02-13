using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Exceptions;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public class ProviderService : IProviderService
    {
        public async Task<int> GetProviderPatientVisitCount(DateTime startDate, DateTime endDate)
        {
            int resp = await DataUtility.GetProviderPatientVisitCountAsync(SettingsValues.ApiURLValue, (int)Globals.Instance.UserInfo.ProviderID, startDate, endDate, CommonAuthSession.Token);
            return resp;
        }

        public async Task<GetProviderProfileResponse> GetProviderProfile()
        {
            return await DataUtility.GetProviderProfileInfoAsync(SettingsValues.ApiURLValue, (int)Globals.Instance.UserInfo.ProviderID, CommonAuthSession.Token);
        }

        public async Task<string> GetProviderResponseTime(DateTime startDate, DateTime endDate)
        {
            string resp = await DataUtility.GetProviderResponseTimeAsync(SettingsValues.ApiURLValue, (int)Globals.Instance.UserInfo.ProviderID, startDate, endDate, CommonAuthSession.Token);
            return resp;
        }

        public async Task<SpecialtiesResponse> GetProviderSpecialties()
        {
            return await DataUtility.GetSpecialtiesAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token);
        }

        public async Task<bool> UpdateProviderInformation(GetProviderProfileResponse profileResponse, Specialty specialty)
        {
            StatusResponse resp = await DataUtility.UpdateProviderProfileAsync(SettingsValues.ApiURLValue, (int)Globals.Instance.UserInfo.ProviderID,
                profileResponse.FirstName,
                profileResponse.LastName,
                profileResponse.DOB,
                profileResponse.Gender,
                profileResponse.Email,
                profileResponse.Phone,
                profileResponse.Street1,
                profileResponse.Street2,
                profileResponse.City,
                profileResponse.State,
                profileResponse.Zip,
                profileResponse.Notes,
                profileResponse.MedicalSchool,
                profileResponse.Degree,
                profileResponse.GraduationDate,
                specialty,
                CommonAuthSession.Token);

            if (resp.StatusCode != StatusCode.Success)
                throw new ProviderException(resp.Message);

            return resp.StatusCode == StatusCode.Success;
        }

        public async Task<GetProviderStatsResponse> GetProviderStatsAsync(int providerId)
        {
            return await DataUtility.GetProviderStatsAsync(SettingsValues.ApiURLValue, providerId, CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<StatusResponse> SetStatusAsync(int providerId, bool availability)
        {
            return await DataUtility.SetStatusAsync(SettingsValues.ApiURLValue, providerId, availability, CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<ProviderPreferences> GetProviderPreferencesAsync(int providerId)
        {
            return await DataUtility.GetProviderPreferencesAsync(SettingsValues.ApiURLValue, providerId, CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<List<SecurityQuestion>> GetSecurityQuestions()
        {
            return await DataUtility.GetSecurityQuestions(SettingsValues.ApiURLValue).ConfigureAwait(false);
        }

        public async Task<StatusResponse> UpdatePreferences(int providerId, int minAge, int maxAge, string notificationPreference, int repeatAlarms, List<SecurityQuestion> securityQquestions)
        {
            StatusResponse resp = await DataUtility.UpdatePreferencesAsync(SettingsValues.ApiURLValue, providerId, minAge, maxAge, notificationPreference, repeatAlarms, securityQquestions, CommonAuthSession.Token).ConfigureAwait(false);

            if (resp.StatusCode != StatusCode.Success)
                throw new ProviderException(resp.Message);

            return resp;
        }

		public async Task<StatusResponse> UploadProviderImageAsync(byte[] imageBytes)
		{
			StatusResponse resp = await DataUtility.UploadProviderImageAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, imageBytes).ConfigureAwait(false);
			if (resp.StatusCode != StatusCode.SuccessSeePayload)
				throw new ProviderException(resp.Message);

			return resp;
		}
	}
}