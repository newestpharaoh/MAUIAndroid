using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.ViewModels;

namespace CommonLibraryCoreMaui.Services
{
    public interface IVisitsService
    {
		Task<ProviderVisitItemViewModel> GetAllProviderVisits(string sProviderId, VisitStatus visitStatus, string searchText = null);
		Task<List<AbsenceNote>> GetVisitAbsenceNotes(string visitId);
		Task<AbsenceNote> GetAbsenceNotesById(int absenceNoteId);
		Task<bool> UpdateAbsenceNote(AbsenceNote absenceNote);
		Task<bool> DeleteAbsenceNote(int absenceNoteId);
		Task<List<Addendum>> GetVisitAddendaList(string visitId);
		Task<Addendum> GetAddendaById(int addendumId);
		Task<bool> UpdateVisitAddendum(Addendum addendum);
		Task<bool> DeleteVisitAddendum(int addendumId);
		Task<ProviderVisitItemViewModel> GetVisitsAsync(int providerId, VisitStatus visitStatus, string searchText, int startIndex, int endIndex);
        Task<StatusResponse> GetRemainingVisitCount(int loginId);
        Task<PatientsForVisit> PatientStartVisitStep1(int loginId);
        Task<MedicalInfo> PatientStartVisitStep2(int patientId);
        Task<List<ProviderInfo>> PatientStartVisitStep3(int patientId);
        Task<List<GenericRecord>> PatientStartVisitStep4();
        Task<VisitDetailsResponse> PatientStartVisitStep5(StartVisit startVisit);
        Task<VisitInfo> PatientRestartVisit(int visitId, string locale);
        Task<ActiveVisitInfo> GetPatientActiveVisits(int PatientID, int GuardianID, string GuardianName = "");
        Task<StatusResponse> ProviderEndVisitAsync(int visitId);
      
        Task<VisitDetailsResponse> GetVisitDetailAsync(int visitID);

        Task<VisitInfo> UpdateLastActiveVisitTime(int VisitID);

    }

    public interface ICapsuleService
    {
        Task<bool> GetCapsuleEligibility(int patientId);
        Task<bool> GetZipCapsuleEligibility(string zip);
        Task<StatusResponse> UpdateCapsuleEligibility(int patientId);
    }
}