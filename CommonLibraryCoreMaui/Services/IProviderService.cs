using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public interface IProviderService
    {
        Task<GetProviderProfileResponse> GetProviderProfile();
        Task<SpecialtiesResponse> GetProviderSpecialties();
        Task<bool> UpdateProviderInformation(GetProviderProfileResponse profileResponse, Specialty specialty);
        Task<string> GetProviderResponseTime(DateTime startDate, DateTime endDate);
        Task<int> GetProviderPatientVisitCount(DateTime startDate, DateTime endDate);
        Task<GetProviderStatsResponse> GetProviderStatsAsync(int providerId);
        Task<StatusResponse> SetStatusAsync(int providerId, bool availability);
        Task<ProviderPreferences> GetProviderPreferencesAsync(int providerId);
        Task<List<SecurityQuestion>> GetSecurityQuestions();
        Task<StatusResponse> UpdatePreferences(int providerId, int minAge, int maxAge, string notificationSettings, int repeatAlerts, List<SecurityQuestion> securityQuestions);
		Task<StatusResponse> UploadProviderImageAsync(byte[] imageBytes);
	}
}