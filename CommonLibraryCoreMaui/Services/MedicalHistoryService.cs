using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;

namespace CommonLibraryCoreMaui.Services
{
    public class MedicalHistoryService : IMedicalHistoryService
    {
        public async Task<List<MedicalIssue>> GetMedicalIssues()
        {
            return await DataUtility.GetMedicalIssuesAsync(SettingsValues.ApiURLValue).ConfigureAwait(false);
        }

        public async Task<MedicalInfo> PatientGetMedicalHistory(int patientId)
        {
            return await DataUtility.PatientGetMedicalHistoryAsync(SettingsValues.ApiURLValue, CommonAuthSession.Token, patientId).ConfigureAwait(false);
        }

		public async Task<StatusResponse> UpdateMedicalHistoryAsync(MedicalInfo medicalInfo)
		{
            return await DataUtility.UpdateMedicalHistoryAsync(SettingsValues.ApiURLValue, medicalInfo, CommonAuthSession.Token).ConfigureAwait(false);
        }
	}
}
