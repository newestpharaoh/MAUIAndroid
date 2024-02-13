using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Exceptions;
using CommonLibraryCoreMaui.Models;
using CommonLibraryCoreMaui.Models.ViewModels;

namespace CommonLibraryCoreMaui.Services
{
    public class VisitsService : IVisitsService
    {
        public async Task<ProviderVisitItemViewModel> GetAllProviderVisits(string sProviderId, VisitStatus visitStatus, string searchText = null)
        {
            var itemList = new List<ProviderVisitItem>();

            ProviderVisitsResponse resp = await DataUtility.GetVisitsAsync(SettingsValues.ApiURLValue, sProviderId, visitStatus, CommonAuthSession.Token, searchText);

            if (resp == null)
                throw new ProviderException("No response data from data utility");

            foreach (Visit v in resp.Visits)
            {
                ProviderVisitItem itemVisit = new ProviderVisitItem
                {
                    VisitID = v.VisitID.ToString(),
                    Status = v.Status,
                    PatientFirstName = v.PatientFirstName,
                    PatientLastName = v.PatientLastName,
                    ProviderName = v.ProviderName,
                    StartTime = v.StartTime,
                    EndTime = v.EndTime,
                    ReasonForVisit = v.ReasonForVisit
                };

                itemList.Add(itemVisit);
            }

            return new ProviderVisitItemViewModel() { Items = itemList, TotalVisitCount = resp.TotalVisitCount };
        }

        public async Task<List<AbsenceNote>> GetVisitAbsenceNotes(string visitId)
        {
            var itemList = new List<AbsenceNote>();

            var resp = await DataUtility.GetVisitAbsenceNotesAsync(SettingsValues.ApiURLValue, visitId, CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data from data utility");

            return resp;
        }

        public async Task<List<Addendum>> GetVisitAddendaList(string visitId)
        {
            var itemList = new List<Addendum>();

            var resp = await DataUtility.GetVisitAddendaListAsync(SettingsValues.ApiURLValue, visitId, CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data from data utility");

            return resp;
        }

        public async Task<Addendum> GetAddendaById(int addendumId)
        {
            var resp = await DataUtility.GetVisitAddendumAsync(SettingsValues.ApiURLValue, addendumId, CommonAuthSession.Token);
            if (resp == null)
                throw new ProviderException("No response data for addenda");

            return resp;
        }

        public async Task<AbsenceNote> GetAbsenceNotesById(int absenceNoteId)
        {
            var resp = await DataUtility.GetVisitAbsenceNoteAsync(SettingsValues.ApiURLValue, absenceNoteId, CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data for Absence Note");

            return resp;
        }

        public async Task<AbsenceNote> UpdateAbsenceNoteAsync(int absenceNoteId)
        {
            var resp = await DataUtility.GetVisitAbsenceNoteAsync(SettingsValues.ApiURLValue, absenceNoteId, CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data for Absence Note");

            return resp;
        }

        public async Task<ProviderVisitItemViewModel> GetVisitsAsync(int providerId, VisitStatus visitStatus, string searchText, int startIndex, int endIndex)
        {
            var itemList = new List<ProviderVisitItem>();

            var resp = await DataUtility.GetVisitsAsync(SettingsValues.ApiURLValue, providerId.ToString(), visitStatus, CommonAuthSession.Token, searchText, startIndex, endIndex);

            if (resp == null)
                throw new ProviderException("No response data for visits");

            foreach (Visit v in resp.Visits)
            {
                ProviderVisitItem itemVisit = new ProviderVisitItem
                {
                    VisitID = v.VisitID.ToString(),
                    Status = v.Status,
                    PatientFirstName = v.PatientFirstName,
                    PatientLastName = v.PatientLastName,
                    ProviderName = v.ProviderName,
                    StartTime = v.StartTime,
                    EndTime = v.EndTime,
                    ReasonForVisit = v.ReasonForVisit
                };

                itemList.Add(itemVisit);
            }

            return new ProviderVisitItemViewModel() { Items = itemList, TotalVisitCount = resp.TotalVisitCount };
        }

        public async Task<bool> UpdateAbsenceNote(AbsenceNote absenceNote)
        {
            var resp = await DataUtility.UpdateAbsenceNoteAsync(SettingsValues.ApiURLValue,
                absenceNote.VisitID.ToString(),
                (int)absenceNote.AbsenceNoteID,
                absenceNote.PatientName,
                absenceNote.ReturnText,
                absenceNote.ProviderName,
                absenceNote.RestrictionText,
                absenceNote.Link,
                absenceNote.Other,
                CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data for Absence Note");

            return resp.StatusCode == StatusCode.Success;
        }

        public async Task<bool> DeleteAbsenceNote(int absenceNoteId)
        {
            var resp = await DataUtility.DeleteVisitAbsencesNoteAsync(SettingsValues.ApiURLValue, absenceNoteId, CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data for Absence Note");

            return resp.StatusCode == StatusCode.Success;
        }

        public async Task<bool> UpdateVisitAddendum(Addendum addendum)
        {
            var resp = await DataUtility.UpdateVisitAddendumAsync(SettingsValues.ApiURLValue,
                addendum.VisitID.ToString(),
                addendum.AddendumID.GetValueOrDefault(),
                addendum.ProviderName,
                addendum.TimeEntered,
                addendum.Text,
                Globals.Instance.UserInfo.UserID,
                CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data for Absence Note");

            return resp.StatusCode == StatusCode.Success;
        }

        public async Task<bool> DeleteVisitAddendum(int addendumId)
        {
            var resp = await DataUtility.DeleteVisitAddendumAsync(SettingsValues.ApiURLValue, addendumId, CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data for Addendum");

            return resp.StatusCode == StatusCode.Success;
        }

        public async Task<StatusResponse> GetRemainingVisitCount(int loginId)
        {
            return await DataUtility.GetRemainingVisitCountAsync(SettingsValues.ApiURLValue, loginId, CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<PatientsForVisit> PatientStartVisitStep1(int loginId)
        {
            return await DataUtility.PatientStartVisitStep1Async(SettingsValues.ApiURLValue, loginId, CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<MedicalInfo> PatientStartVisitStep2(int patientId)
        {
            return await ApiUtility.SendApiRequestAsync<MedicalInfo>(SettingsValues.ApiURLValue, $"Visits/PatientStartVisitStep2?PatientID={patientId}", CommonAuthSession.Token, null, RestSharp.Method.Get).ConfigureAwait(false);
        }

        public async Task<List<ProviderInfo>> PatientStartVisitStep3(int patientId)
        {
            return await ApiUtility.SendApiRequestAsync<List<ProviderInfo>>(SettingsValues.ApiURLValue, $"Visits/PatientStartVisitStep3?PatientID={patientId}", CommonAuthSession.Token, null, RestSharp.Method.Get).ConfigureAwait(false);
        }

        public async Task<List<GenericRecord>> PatientStartVisitStep4()
        {
            return await ApiUtility.SendApiRequestAsync<List<GenericRecord>>(SettingsValues.ApiURLValue, "Visits/PatientStartVisitStep4", CommonAuthSession.Token, null, RestSharp.Method.Get).ConfigureAwait(false);
        }

        public async Task<VisitDetailsResponse> PatientStartVisitStep5(StartVisit startVisit)
        {
            return await ApiUtility.SendApiRequestPostAsync<VisitDetailsResponse>(SettingsValues.ApiURLValue, "Visits/PatientStartVisitStep5", CommonAuthSession.Token, startVisit).ConfigureAwait(false);
        }

        public async Task<StatusResponse> ProviderEndVisitAsync(int visitId)
        {
            return await ApiUtility.SendApiRequestAsync<StatusResponse>(SettingsValues.ApiURLValue, $"Visits/ProviderEndVisit?VisitID={visitId}", CommonAuthSession.Token, null, RestSharp.Method.Get).ConfigureAwait(false);
        }

        public async Task<VisitDetailsResponse> GetVisitDetailAsync(int visitID)
        {
            VisitDetailsResponse resp = await DataUtility.GetVisitDetailAsync(SettingsValues.ApiURLValue, visitID.ToString(), CommonAuthSession.Token);

            if (resp == null)
                throw new ProviderException("No response data for Visit Detail");

            return resp;
        }    
                      

        public async Task<ActiveVisitInfo> GetPatientActiveVisits(int PatientID, int GuardianID, string GuardianName = "")
        {
            ActiveVisitInfo resp = await DataUtility.GetPatientActiveVisits(SettingsValues.ApiURLValue, PatientID,GuardianID,GuardianName, CommonAuthSession.Token);
            if (resp == null)
                throw new ProviderException("No response data for Visit Detail");
            return resp;          
        }

        public async Task<VisitInfo> PatientRestartVisit(int visitId,string locale="en")
        {
            var resp = await ApiUtility.SendApiRequestAsync<VisitInfo>(SettingsValues.ApiURLValue, $"Visits/PatientRestartVisit?VisitID={visitId}&LocaleID={locale}", CommonAuthSession.Token, null, RestSharp.Method.Get);
            return resp;
        }

        public async Task<VisitInfo> UpdateLastActiveVisitTime(int visitId)
        {
            VisitInfo resp = await ApiUtility.SendApiRequestAsync<VisitInfo>(SettingsValues.ApiURLValue, $"Visits/UpdateLastActiveVisitTime?VisitID={visitId}", CommonAuthSession.Token, null, RestSharp.Method.Get);
            return resp;
        }
    }

    public class CapsuleService : ICapsuleService
    {
        public async Task<bool> GetCapsuleEligibility(int patientId)
        {
            return await DataUtility.GetCapsuleEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, patientId, CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<bool> GetZipCapsuleEligibility(string zip)
        {
            return await DataUtility.GetZipCapsuleEligibilityAsync(SettingsValues.ApiURLValue, zip, CommonAuthSession.Token).ConfigureAwait(false);
        }

        public async Task<StatusResponse> UpdateCapsuleEligibility(int patientId)
        {
            return await DataUtility.UpdateCapsuleEligibilityForHomeViewDialogAsync(SettingsValues.ApiURLValue, patientId, CommonAuthSession.Token).ConfigureAwait(false);
        }
    }
}