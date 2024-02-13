using CommonLibraryCoreMaui.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.Services
{
    public interface IMedicalHistoryService
    {
        Task<MedicalInfo> PatientGetMedicalHistory(int patientId);
        Task<List<MedicalIssue>> GetMedicalIssues();
        Task<StatusResponse> UpdateMedicalHistoryAsync(MedicalInfo medicalInfo);
    }
}
