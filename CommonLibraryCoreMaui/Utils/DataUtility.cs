using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using CommonLibraryCoreMaui.Models;
using Newtonsoft.Json;

namespace CommonLibraryCoreMaui
{
    public static class DataUtility
    {
        public static async Task<ForgotPasswordGetSecurityQuestionResponse> ForgotPasswordGetSecurityQuestion(string sApiDomainURL, string firstName, string lastName, string dob, string email)
        {
            return await ApiUtility.SendApiRequestAsync<ForgotPasswordGetSecurityQuestionResponse>(sApiDomainURL, string.Format("/ForgotPassword/GetSecurityQuestion?firstname={0}&lastname={1}&dob={2}&isPatient=true&email={3}", firstName, lastName, dob, email), null, null, RestSharp.Method.Get);
        }

        public static async Task<StatusResponse> ForgotPasswordCheckSecurityQuestion(string sApiDomainURL, int userId, int questionId, string answer)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("UserID", userId.ToString());
            httpReqParamValues.Add("QuestionID", questionId.ToString());
            httpReqParamValues.Add("AnswerText", answer);

            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/ForgotPassword/CheckSecurityQuestion", null, httpReqParamValues);
            return resp;
        }

        public static async Task<StatusResponse> ForgotPasswordResetPassword(string sApiDomainURL, int userId, string password)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("UserID", userId.ToString());
            httpReqParamValues.Add("Password", password);

            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/ForgotPassword/ResetPassword", null, httpReqParamValues);

            return resp;
        }

        public static async Task<StatusResponse> UserAccountSendCode(string sApiDomainURL, int userId, string sendMethod, string optionalEmailForTesting, string token)
        {
            return await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/UserAccount/SendCode?UserID={0}&sendMethod={1}&optionalEmailForTesting={2}&IsPatient=true", userId, sendMethod, optionalEmailForTesting), null, null, RestSharp.Method.Get);
        }

        public static async Task<StatusResponse> UserAccountAuthenticateCode(string sApiDomainURL, int userId, string code)
        {
            return await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/UserAccount/AuthenticateCode?UserID={0}&Code={1}", userId, code), null, null, RestSharp.Method.Get);
        }

        public static async Task<UserInfo> GetUserInfo(string sApiDomainURL, int userId, bool isPatient, string token)
        {
            UserInfo resp = await ApiUtility.SendApiRequestAsync<UserInfo>(sApiDomainURL, string.Format("/UserAccount/GetUserInfo?UserID={0}&isPatient={1}", userId, isPatient), token, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<StatusResponse> AddQuestionnaire(string sApiDomainURL, int visitId, string encounterType, string medicationPrescribed, bool labOrdered, bool RadiologyOrdered, bool pCPFollowUp, bool specialistFollowUp, string specialistName, bool sentToER, bool sentToClinic, bool telemedFollowUp, string telemedFollowUpDate, string token)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("VisitID", visitId.ToString());
            httpReqParamValues.Add("EncounterType", encounterType);
            httpReqParamValues.Add("MedicationPrescribed", medicationPrescribed);
            httpReqParamValues.Add("LabOrdered", labOrdered.ToString());
            httpReqParamValues.Add("RadiologyOrdered", RadiologyOrdered.ToString());
            httpReqParamValues.Add("PCPFollowUp", pCPFollowUp.ToString());
            httpReqParamValues.Add("SpecialistFollowUP", specialistFollowUp.ToString());
            httpReqParamValues.Add("SpecialistName", specialistName);
            httpReqParamValues.Add("SentToER", sentToER.ToString());
            httpReqParamValues.Add("SentToClinic", sentToClinic.ToString());
            httpReqParamValues.Add("TelemedFollowUp", telemedFollowUp.ToString());
            httpReqParamValues.Add("TelemedFollowUpDate", telemedFollowUpDate);

            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Visits/AddQuestionnaire", token, httpReqParamValues);
            return resp;
        }
        public static async Task<ActiveVisitInfo> GetPatientActiveVisits(string sApiDomainURL, int PatientID, int GuardianID, string GuardianName, string sToken)
        {
            ActiveVisitInfo resp = await ApiUtility.SendApiRequestAsync<ActiveVisitInfo>(sApiDomainURL,
                $"/Visits/GetPatientActiveVisits?PatientID={PatientID}&GuardianID={GuardianID}&GuardianName={GuardianName}", sToken, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<VisitDetailsResponse> GetVisitDetailAsync(string sApiDomainURL, string sVisitID, string sToken)
        {
            VisitDetailsResponse resp = await ApiUtility.SendApiRequestAsync<VisitDetailsResponse>(sApiDomainURL, $"/Visits/Visit?visitID={sVisitID}", sToken, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<StatusResponse> UpdateVisitAsync(string sApiDomainURL, int visitId, string note, string diagnosis1, string diagnosis2, string sToken)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("VisitID", visitId.ToString());
            httpReqParamValues.Add("Note", note);
            httpReqParamValues.Add("Diagnosis1ID", diagnosis1);
            httpReqParamValues.Add("Diagnosis2ID", diagnosis2);
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Visits/UpdateVisit", sToken, httpReqParamValues);
            return resp;
        }

        public static async Task<StatusResponse> CompleteVisitAsync(string sApiDomainURL, int visitId, string note, string diagnosis1, string diagnosis2, string sToken)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("VisitID", visitId.ToString());
            httpReqParamValues.Add("Note", note);
            httpReqParamValues.Add("Diagnosis1ID", diagnosis1);
            httpReqParamValues.Add("Diagnosis2ID", diagnosis2);
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Visits/CompleteVisit", sToken, httpReqParamValues);
            return resp;
        }

        public static async Task<StatusResponse> SetStatusAsync(string sApiDomainURL, int providerId, bool available, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Provider/SetStatus?providerID={0}&available={1}", providerId, available), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> SetLoggedInAsync(string sApiDomainURL, int providerId, bool loggedin, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Provider/SetLoggedIn?providerID={0}&loggedin={1}", providerId, loggedin), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<GetProviderStatsResponse> GetProviderStatsAsync(string sApiDomainURL, int providerId, string token)
        {
            GetProviderStatsResponse resp = await ApiUtility.SendApiRequestAsync<GetProviderStatsResponse>(sApiDomainURL, string.Format("/Provider/Stats?providerID={0}", providerId), token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<GetProviderProfileResponse> GetProviderProfileInfoAsync(string sApiDomainURL, int providerId, string token)
        {
            GetProviderProfileResponse resp = await ApiUtility.SendApiRequestAsync<GetProviderProfileResponse>(sApiDomainURL, string.Format("/Provider/GetProfile?providerID={0}", providerId), token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> UpdateProviderPasswordAsync(string sApiDomainURL, string sProviderID, string sCurrentPassword, string sNewPassword, string sToken)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            if (!string.IsNullOrEmpty(sProviderID)) httpReqParamValues.Add("ID", sProviderID);
            httpReqParamValues.Add("CurrentPassword", sCurrentPassword);
            httpReqParamValues.Add("NewPassword", sNewPassword);
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Provider/UpdatePassword", sToken, httpReqParamValues);
            return resp;
        }

        public static async Task<List<SecurityQuestion>> GetSecurityQuestions(string sApiDomainURL)
        {
            List<SecurityQuestion> resp = await ApiUtility.SendApiRequestAsync<List<SecurityQuestion>>(sApiDomainURL, "/SecurityQuestions", null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> UpdatePreferencesAsync(string sApiDomainURL, int providerId, int minAge, int maxAge, string sNotificationPreference, int repeatAlerts, string sSecurityQuestionAnswer, string sToken)
        {
            ProviderPreferences req = new ProviderPreferences();
            List<SecurityQuestion> list = JsonConvert.DeserializeObject<List<SecurityQuestion>>(sSecurityQuestionAnswer);

            req.SecurityQuestionAnswer = list;
            req.NotificationPreference = sNotificationPreference;
            req.MaxAge = maxAge;
            req.MinAge = minAge;
            req.providerID = providerId;
            req.RepeatAlerts = repeatAlerts;

            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Provider/UpdatePreferences", sToken, req);
            return resp;
        }

        public static async Task<StatusResponse> UpdatePreferencesAsync(string sApiDomainURL, int providerId, int minAge, int maxAge, string sNotificationPreference, int repeatAlerts, List<SecurityQuestion> securityQuestionAnswer, string sToken)
        {
            ProviderPreferences req = new ProviderPreferences();
            req.SecurityQuestionAnswer = securityQuestionAnswer;
            req.NotificationPreference = sNotificationPreference;
            req.MaxAge = maxAge;
            req.MinAge = minAge;
            req.providerID = providerId;
            req.RepeatAlerts = repeatAlerts;

            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Provider/UpdatePreferences", sToken, req);
            return resp;
        }

        public static async Task<StatusResponse> DeleteVisitAddendumAsync(string sApiDomainURL, string addendumID, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Visits/DeleteVisitAddendum?addendumID={0}", addendumID), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> DeleteVisitAbsenceNoteAsync(string sApiDomainURL, string absenceNoteID, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Visits/DeleteVisitAbsencesNote?absenceNoteID={0}", absenceNoteID), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> LogoutAsync(string sApiDomainURL, string providerID, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Provider/Logout?providerID={0}", providerID), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderPreferences> GetProviderPreferencesAsync(string sApiDomainURL, string providerID, string sToken)
        {
            ProviderPreferences resp = await ApiUtility.SendApiRequestAsync<ProviderPreferences>(sApiDomainURL, string.Format("/Provider/GetPreferences?providerID={0}", providerID), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderWaitListResponse> GetProviderWaitListAsync(string sApiDomainURL, string sProviderId, string sToken)
        {
            ApiUtility.RaiseProgressEvents = false;
            ProviderWaitListResponse resp = await ApiUtility.SendApiRequestAsync<ProviderWaitListResponse>(sApiDomainURL, string.Format("/Visits/WaitList?providerID={0}&completed=true", sProviderId), sToken, null, RestSharp.Method.Get);
            ApiUtility.RaiseProgressEvents = true;
            return resp;
        }

        public static async Task<ProviderActiveVisitsResponse> GetProviderActiveVisitsAsync(string sApiDomainURL, string sProviderId, string sToken)
        {
            ProviderActiveVisitsResponse resp = await ApiUtility.SendApiRequestAsync<ProviderActiveVisitsResponse>(sApiDomainURL, string.Format("/Visits/GetProviderActiveVisits?providerID={0}", sProviderId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderVisitsResponse> GetVisitsAsync(string sApiDomainURL, string sProviderId, VisitStatus visitStatus, string sToken, string searchText = null, int? startIndex = null, int? endIndex = null)
        {
            string query = visitStatus == VisitStatus.Completed ? "&completed=true" : "&completed=false";
            if (!string.IsNullOrEmpty(searchText))
            {
                query = string.Concat(query, string.Format("&search={0}", searchText));
            }
            if (startIndex != null && endIndex != null)
            {
                query = string.Concat(query, string.Format("&takeAll=false&startIndex={0}&endIndex={1}", startIndex, endIndex));
            }
            ProviderVisitsResponse resp = await ApiUtility.SendApiRequestAsync<ProviderVisitsResponse>(sApiDomainURL, string.Format("/Visits/ProviderVisitsMobile?providerID={0}{1}", sProviderId, query), sToken, null, RestSharp.Method.Get);
            return resp;
        }
        //PatientRestartVisit
        public static async Task<string> GetLockoutTimeAsync(string sApiDomainURL, string appId)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, string.Format("/UserAccount/GetLockoutTime?IP=0", appId), null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<QuickPhraseListResponse> UpdateProviderQuickPhraseAsync(string sApiDomainURL, int sProviderID, int sPhraseID, string quickPhraseTitle, string sPhrase, int sSortOrder, bool sIsDisplayed, string sToken)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("ProviderID", sProviderID.ToString());
            httpReqParamValues.Add("PhraseID", sPhraseID.ToString());
            httpReqParamValues.Add("Phrase", sPhrase);
            httpReqParamValues.Add("PhraseTitle", quickPhraseTitle);
            httpReqParamValues.Add("SortOrder", sSortOrder.ToString());
            httpReqParamValues.Add("IsDisplayed", sIsDisplayed.ToString());

            QuickPhraseListResponse resp = await ApiUtility.SendApiRequestPostAsync<QuickPhraseListResponse>(sApiDomainURL, "/Provider/UpdateProviderQuickPhrase", sToken, httpReqParamValues);
            return resp;
        }

        public static async Task<ProviderDocumentsResponse> UpdateProviderDocumentSortOrderAsync(string sApiDomainURL, string sToken, int providerDocumentId, int sortOrder)
        {
            ProviderDocumentsResponse resp = await ApiUtility.SendApiRequestAsync<ProviderDocumentsResponse>(sApiDomainURL, string.Format("/Provider/UpdateProviderDocumentSortOrder?providerDocumentID={0}&sortOrder={1}", providerDocumentId, sortOrder), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderStartVisitResponse> ProviderStartVisitAsync(string sApiDomainURL, string sToken, int visitId, int providerId)
        {
            ProviderStartVisitResponse resp = await ApiUtility.SendApiRequestAsync<ProviderStartVisitResponse>(sApiDomainURL, string.Format("/Visits/ProviderStartVisit?VisitID={0}&ProviderID={1}", visitId, providerId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        #region Medical Issues

        public static async Task<MedicalInfo> PatientGetMedicalHistoryAsync(string sApiDomainURL, string sToken, int patientId)
        {
            MedicalInfo resp = await ApiUtility.SendApiRequestAsync<MedicalInfo>(sApiDomainURL, string.Format("/Patient/GetMedicalHistory?PatientID={0}", patientId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<MedicalIssue> GetMedicalIssueAsync(string sApiDomainURL, string sToken, int issueId)
        {
            MedicalIssue resp = await ApiUtility.SendApiRequestAsync<MedicalIssue>(sApiDomainURL, string.Format("/MedicalIssue/GetMedicalIssue?ID={0}", issueId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<List<MedicalIssue>> GetMedicalIssuesAsync(string sApiDomainURL)
        {
            List<MedicalIssue> resp = await ApiUtility.SendApiRequestAsync<List<MedicalIssue>>(sApiDomainURL, "/MedicalIssue/GetMedicalIssues", null, null, RestSharp.Method.Get);
            return resp;
        }

        #endregion

        public static async Task<QuickPhraseListResponse> GetProviderQuickPhrasesAsync(string sApiDomainURL, int providerId, string sToken)
        {
            QuickPhraseListResponse resp = await ApiUtility.SendApiRequestAsync<QuickPhraseListResponse>(sApiDomainURL, string.Format("/Provider/GetProviderQuickPhrases?providerID={0}", providerId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderDocumentsResponse> GetProviderDocumentsAsync(string sApiDomainURL, int providerId, string sToken)
        {
            ProviderDocumentsResponse resp = await ApiUtility.SendApiRequestAsync<ProviderDocumentsResponse>(sApiDomainURL, string.Format("/Provider/GetProviderDocuments?providerID={0}", providerId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderDocumentsResponse> DeleteProviderDocumentAsync(string sApiDomainURL, int providerDocumentId, string sToken)
        {
            ProviderDocumentsResponse resp = await ApiUtility.SendApiRequestAsync<ProviderDocumentsResponse>(sApiDomainURL, string.Format("/Provider/DeleteProviderDocument?providerDocumentID={0}", providerDocumentId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<List<ICDCode>> GetICD10CodesAsync(string sApiDomainURL, string sToken)
        {
            List<ICDCode> resp = await ApiUtility.SendApiRequestAsync<List<ICDCode>>(sApiDomainURL, "/Icd10Codes/GetCodes", sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderPreferences> GetProviderPreferencesAsync(string sApiDomainURL, int providerId, string sToken)
        {
            ProviderPreferences resp = await ApiUtility.SendApiRequestAsync<ProviderPreferences>(sApiDomainURL, string.Format("/Provider/GetPreferences?providerID={0}", providerId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<SpecialtiesResponse> GetSpecialtiesAsync(string sApiDomainURL, string sToken)
        {
            SpecialtiesResponse resp = await ApiUtility.SendApiRequestAsync<SpecialtiesResponse>(sApiDomainURL, "/Provider/GetSpecialties", sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> UpdateProviderProfileAsync(string sApiDomainURL, int providerId, string sFirstName, string sLastName, string sDOB, string sGender, string sEmail, string sPhone, string sStreet1, string sStreet2, string sCity, string sState, string sZip, string sNotes, string sMedicalSchool, string sDegree, string sGraduationDate, Specialty specialty, string token)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("ProviderID", providerId.ToString());
            httpReqParamValues.Add("FirstName", sFirstName);
            httpReqParamValues.Add("LastName", sLastName);
            httpReqParamValues.Add("DOB", sDOB);
            httpReqParamValues.Add("Gender", sGender.Substring(0, 1));
            httpReqParamValues.Add("Email", sEmail);
            httpReqParamValues.Add("Phone", sPhone);
            httpReqParamValues.Add("Street1", sStreet1);
            httpReqParamValues.Add("Street2", sStreet2);
            httpReqParamValues.Add("City", sCity);
            httpReqParamValues.Add("State", sState);
            httpReqParamValues.Add("Zip", sZip);
            httpReqParamValues.Add("Notes", sNotes);
            httpReqParamValues.Add("MedicalSchool", sMedicalSchool);
            httpReqParamValues.Add("Degree", sDegree);
            httpReqParamValues.Add("GraduationDate", sGraduationDate);
            httpReqParamValues.Add("SpecialtyID", specialty is null ? "0" : specialty.ID.ToString());
            httpReqParamValues.Add("SpecialtyName", specialty is null ? "" : specialty.Value.ToString());

            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Provider/UpdateProfile", token, httpReqParamValues);
            return resp;
        }

        public static async Task<AbsenceNote> GetVisitAbsenceNoteAsync(string sApiDomainURL, int absenceNoteId, string sToken)
        {
            AbsenceNote resp = await ApiUtility.SendApiRequestAsync<AbsenceNote>(sApiDomainURL, string.Format("/Visits/GetVisitAbsenceNote?absencesNoteID={0}", absenceNoteId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> DeleteVisitAbsencesNoteAsync(string sApiDomainURL, int absenceNoteId, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Visits/DeleteVisitAbsencesNote?absenceNoteID={0}", absenceNoteId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<Addendum> GetVisitAddendumAsync(string sApiDomainURL, int addendumId, string sToken)
        {
            Addendum resp = await ApiUtility.SendApiRequestAsync<Addendum>(sApiDomainURL, string.Format("/Visits/GetVisitAddendum?addendumID={0}", addendumId), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> UpdateVisitAddendumAsync(string sApiDomainURL, string sVisitID, int sAddendumID, string sProviderName, string sTimeEntered, string sText, int sUserID, string sToken)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("AddendumID", sAddendumID == 0 ? string.Empty : sAddendumID.ToString());
            httpReqParamValues.Add("VisitID", sVisitID);
            httpReqParamValues.Add("TimeEntered", sTimeEntered);
            httpReqParamValues.Add("Text", sText);
            httpReqParamValues.Add("ProviderName", sProviderName);
            httpReqParamValues.Add("UserID", sUserID.ToString());
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Visits/UpdateVisitAddendum", sToken, httpReqParamValues);
            return resp;
        }

        public static async Task<StatusResponse> DeleteVisitAddendumAsync(string sApiDomainURL, int addendumID, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Visits/DeleteVisitAddendum?addendumID={0}", addendumID), sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> UpdateAbsenceNoteAsync(string sApiDomainURL, string sVisitID, int sAbsenceNoteID, string sForAbsentee, string sToRecipientName, string sProviderName, string sNote, string sLink, bool other, string sToken)
        {
            NameValueCollection httpReqParamValues = new NameValueCollection();
            httpReqParamValues.Add("AbsenceNoteID", sAbsenceNoteID == 0 ? string.Empty : sAbsenceNoteID.ToString());
            httpReqParamValues.Add("VisitID", sVisitID);
            httpReqParamValues.Add("PatientName", sForAbsentee);
            httpReqParamValues.Add("ReturnText", sToRecipientName);
            httpReqParamValues.Add("ProviderName", sProviderName);
            httpReqParamValues.Add("RestrictionText", sNote);
            httpReqParamValues.Add("Link", sLink);
            httpReqParamValues.Add("Other", other.ToString());

            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Visits/UpdateVisitAbsencesNote", sToken, httpReqParamValues);
            return resp;
        }

        public static async Task<SiteSettings> GetSiteSettingsAsync(string sApiDomainURL)
        {
            SiteSettings resp = await ApiUtility.SendApiRequestAsync<SiteSettings>(sApiDomainURL, "/Settings/GetSiteSettings?ID=1", null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<UITopic> GetUITopicListAsync(string sApiDomainURL, string topicName, string locale)
        {
            UITopic resp = await ApiUtility.SendApiRequestAsync<UITopic>(sApiDomainURL, string.Format("/Helper/GetUITopicList?topicName={0}&locale={1}", topicName, locale), null, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<string> GetSurveyMonkeyURL(string sApiDomainURL, string visitID)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, string.Format("/Helper/GetSurveyMonkeyURL?visitID={0}", visitID),
                null, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<string> GetBrandAsync(string sApiDomainURL)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, "/Settings/GetBrand", null, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<TokenResponse> GetTokenResponseAsync(string sApiDomainURL, string username, string pwd, string ip)
        {
            TokenResponse resp = await ApiUtility.SendApiRequestPostAsync<TokenResponse>(sApiDomainURL, "/token", username, pwd, ip);
            return resp;
        }
        public static async Task<string> GetStoreMessageAsync(string sApiDomainURL)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, "/Messaging/StoreMessage", null, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<StatusResponse> PostVisitRequestMessage(string sApiDomainURL, FMChatMessageModel chatMessage, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Messaging/StoreChatMessage_2", sToken, chatMessage);
            return resp;
        }
        public static async Task<StatusResponse> PostChatMessage(string sApiDomainURL, FMChatMessageModel visitRequestMessage, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Messaging/StoreVisitRequestMessage_2", sToken, visitRequestMessage);
            return resp;
        }
        public static async Task<StatusResponse> PostVRSystemMessage(string sApiDomainURL, FMChatMessageModel visitRequestMessage, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Messaging/StoreVRSystemMessage_2", sToken, visitRequestMessage);
            return resp;
        }
        public static async Task<StatusResponse> PostChatSystemMessage_2(string sApiDomainURL, FMChatSystemMessageModel visitRequestSystemMessage, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Messaging/StoreVRSystemMessage_2", sToken, visitRequestSystemMessage);
            return resp;
        }
        public static async Task<string> GetChatVersionResponseAsync(string sApiDomainURL, string sToken)
        {

            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, "/Helper/GetChatVersion", sToken, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<string> GetRelProdDevResponseAsync(string sApiDomainURL, string sToken)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, "/Helper/GetRelProdDev", sToken, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<string> GetLiveSwitchTokenResponseAsync(string sApiDomainURL, string URL, string sToken)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, URL, sToken, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<string> GetLSAppIDAsync(string sApiDomainURL, string sToken)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, "/GetLSAppID", sToken, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<PatientProfile> GetPatientProfileAsync(string sApiDomainURL, string token, int patientId)
        {
            PatientProfile resp = await ApiUtility.SendApiRequestAsync<PatientProfile>(sApiDomainURL, string.Format("/Patient/GetProfile?PatientID={0}", patientId), token, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<StatusResponse> GetActiveCoverage(string sApiDomainURL, int patientId, string token, string locale)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Patient/GetActiveCoverage?patientID={0}", patientId), token, null, RestSharp.Method.Get);
            return resp;
        }
        //Audits the user clicking Cancel Plan Hyperlink from Manage Subscriptions   
        public static async Task<StatusResponse> ClickCancelPlanHyperlink(string sApiDomainURL, int patientId, string token, string locale)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Patient/ClickCancelPlanHyperlink?patientID={0}", patientId), token, null, RestSharp.Method.Get);
            return resp;
        }
        //Audits the user clicking Cancel Plan Button from Cancel Plan Dialog
        public static async Task<StatusResponse> ClickCancelPlanButton(string sApiDomainURL, int patientId, string token, string locale)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Patient/ClickCancelPlanButton?patientID={0}", patientId), token, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<UserContactPreference> GetUserContactPreferenceAsync(string sApiDomainURL, string token, int userId, bool isPatient = true)
        {
            UserContactPreference resp = await ApiUtility.SendApiRequestAsync<UserContactPreference>(sApiDomainURL, string.Format("/UserAccount/GetUserContactPreference?UserID={0}&isPatient={1}", userId, isPatient), token, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<List<AbsenceNote>> GetVisitAbsenceNotesAsync(string sApiDomainURL, string sVisitID, string sToken)
        {
            List<AbsenceNote> resp = await ApiUtility.SendApiRequestAsync<List<AbsenceNote>>(sApiDomainURL, string.Format("/Visits/GetVisitAbsenceNotes?visitID={0}", sVisitID), sToken, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<StatusResponse> AddEncounterNoteAsync(string sApiDomainURL, int visitId, string note, string sToken)
        {
            EncounterNote encounterNote = new EncounterNote();
            encounterNote.Note = note;
            encounterNote.VisitID = visitId.ToString();
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Visits/AddEncounterNote", sToken, encounterNote);
            return resp;
        }
        public static async Task<StatusResponse> AddDiagnosisAsync(string sApiDomainURL, string token, int visitId, int primaryDiagnosisId, int? secondaryDiagnosisId = null)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("/Visits/AddDiagnosis?VisitID={0}&Diagnosis1ID={1}{2}", visitId, primaryDiagnosisId, secondaryDiagnosisId != null ? string.Format("&Diagnosis2ID={0}", (int)secondaryDiagnosisId) : ""), token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> RegistrationStep1Async(string sApiDomainURL, RegistrationUserInfo userInfo)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/UserAccount/RegistrationStep1", null, userInfo);
            return resp;
        }

        public static async Task<StatusResponse> FindPatientAsync(string sApiDomainURL, string firstName, string lastName, string dob, string optionalEmailForTesting = null)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, string.Format("UserAccount/FindPatient?firstname={0}&lastname={1}&dob={2}{3}&locale=en", firstName, lastName, dob, string.IsNullOrEmpty(optionalEmailForTesting) ? "" : $"&optionalEmailForTesting={optionalEmailForTesting}"), null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> RegistrationStep2Async(string sApiDomainURL, RegistrationStep2Request req)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "UserAccount/RegistrationStep2", null, req);
            return resp;
        }

        public static async Task<PatientSubscriptions> GetPatientSubscriptionsAsync(string sApiDomainURL)
        {
            PatientSubscriptions resp = await ApiUtility.SendApiRequestAsync<PatientSubscriptions>(sApiDomainURL, "UserAccount/GetAvailablePatientSubscriptions", null, null, RestSharp.Method.Get);
            PatientSubscriptions subs = new PatientSubscriptions();

            if (resp?.AvailableSubscriptions != null)
            {
                foreach (SubscriptionBase sb in resp.AvailableSubscriptions.Where(x => x.IsAddOn == false))
                {
                    var subscription = SubscriptionsFactory.Get(sb.OptionID);
                    subscription.OptionID = sb.OptionID;
                    subscription.Cost = sb.Cost;
                    subscription.HasAddOn = sb.HasAddOn;
                    subscription.IsActive = sb.IsActive;
                    subscription.IsAddOn = sb.IsAddOn;
                    subscription.MonthlyVisits = sb.MonthlyVisits;
                    subscription.Name = sb.Name;
                    subscription.PlanDescription = sb.PlanDescription;
                    subscription.TotalOptionMembers = sb.TotalOptionMembers;

                    if (subscription is FamilySubscription || subscription is Family365Subscription)
                    {
                        var addon = resp.AvailableSubscriptionAddOns[0];
                        var addonsubscription = SubscriptionsFactory.Get(addon.OptionID);
                        addonsubscription.OptionID = addon.OptionID;
                        addonsubscription.Cost = addon.Cost;
                        addonsubscription.HasAddOn = addon.HasAddOn;
                        addonsubscription.IsActive = addon.IsActive;
                        addonsubscription.IsAddOn = addon.IsAddOn;
                        addonsubscription.MonthlyVisits = addon.MonthlyVisits;
                        addonsubscription.Name = addon.Name;
                        addonsubscription.PlanDescription = addon.PlanDescription;
                        addonsubscription.TotalOptionMembers = addon.TotalOptionMembers;
                        if (subscription is Family365Subscription) ((Family365Subscription)subscription).AddOn = (AdditionalFamilyMemberSubscription)addonsubscription;
                        else
                            ((FamilySubscription)subscription).AddOn = (AdditionalFamilyMemberSubscription)addonsubscription;
                    }

                    subs.AvailableSubscriptions.Add(subscription);
                }
            }
            return subs;
        }

        //it is going to be used in next project phase
        public static async Task<StatusResponse> ValidatePromoCodeAsync(string sApiDomainURL, int subscriptionId, string promoCode)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"UserAccount/ValidatePromoCode?SubscriptionID={subscriptionId}&PromoCode={promoCode}", null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> RegistrationStep3SelfPayAsync(string sApiDomainURL, Registration req)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "UserAccount/RegistrationStep3SelfPay", null, req);
            return resp;
        }

        public static async Task<StatusResponse> _RegistrationStep3SelfPayAsync(string sApiDomainURL, RegistrationState req)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "UserAccount/RegistrationStep3SelfPay", null, req);
            return resp;
        }

        public static async Task<StatusResponse> RegistrationStep3PrePayAsync(string sApiDomainURL, Registration req)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "UserAccount/RegistrationStep3PrePay", null, req);
            return resp;
        }

        public static async Task<StatusResponse> _RegistrationStep3PrePayAsync(string sApiDomainURL, RegistrationState req)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "UserAccount/RegistrationStep3PrePay", null, req);
            return resp;
        }

        public static async Task<StatusResponse> RegistrationStep4Async(string sApiDomainURL, MedicalInfo req)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "UserAccount/RegistrationStep4", null, req);
            return resp;
        }

        public static async Task<StatusResponse> DeletPatientAsync(string sApiDomainURL, int patientId)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"UserAccount/DeletPatient?PatientID-{patientId}", null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<RegistrationUserInfo> StartSelfPayRegistrationAsync(string sApiDomainURL, SelfPayBillingInfo req)
        {
            RegistrationUserInfo resp = await ApiUtility.SendApiRequestPostAsync<RegistrationUserInfo>(sApiDomainURL, "UserAccount/StartSelfPayRegistration", null, req);
            return resp;
        }

        public static async Task<List<PCP>> SearchPCPAsync(string sApiDomainURL, string firstName = null, string lastName = null, string city = null, string state = null)
        {
            List<string> parameters = new List<string>();
            if (!string.IsNullOrEmpty(firstName))
            {
                parameters.Add($"firstName={firstName}");
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                parameters.Add($"lastName={lastName}");
            }
            if (!string.IsNullOrEmpty(city))
            {
                parameters.Add($"city={city}");
            }
            if (!string.IsNullOrEmpty(state))
            {
                parameters.Add($"state={state}");
            }

            string query = string.Empty;

            if (parameters.Count > 0)
            {
                query = $"?{string.Join("&", parameters.ToArray())}";
            }

            List<PCP> resp = await ApiUtility.SendApiRequestAsync<List<PCP>>(sApiDomainURL, $"PCP/Search{query}", null, null, RestSharp.Method.Get);

            return resp;
        }




        public static async Task<List<Pharmacy>> SearchPharmacyAsync(string sApiDomainURL, string BusinessName = null, string StreetAddress = null, string city = null, string state = null, string zipcode = "", string locale = "")
        {
            List<string> parameters = new List<string>(); if (!string.IsNullOrEmpty(BusinessName)) { parameters.Add($"BusinessName={BusinessName}"); }
            if (!string.IsNullOrEmpty(StreetAddress)) { parameters.Add($"StreetAddress={StreetAddress}"); }
            if (!string.IsNullOrEmpty(city)) { parameters.Add($"city={city}"); }
            if (!string.IsNullOrEmpty(state)) { parameters.Add($"state={state}"); }
            if (!string.IsNullOrEmpty(zipcode)) { parameters.Add($"zipcode={zipcode}"); }
            string query = string.Empty; if (parameters.Count > 0) { query = $"?{string.Join("&", parameters.ToArray())}"; }
            List<Pharmacy> resp = await ApiUtility.SendApiRequestAsync<List<Pharmacy>>(sApiDomainURL, $"Pharmacy/Search{query}", null, null, RestSharp.Method.Get); return resp;
        }
        public static async Task<StatusResponse> SendRegistrationCodeAsync(string sApiDomainURL, int verificationId, string firstName, string lastName, SendMethod sendMethod, string phone = null, string email = null)
        {
            List<string> parameters = new List<string>();

            if (!string.IsNullOrEmpty(phone))
            {
                parameters.Add($"phone={phone}");
            }
            if (!string.IsNullOrEmpty(firstName))
            {
                parameters.Add($"firstName={firstName}");
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                parameters.Add($"lastName={lastName}");
            }
            if (!string.IsNullOrEmpty(email))
            {
                parameters.Add($"email={email}");
            }

            string query = string.Empty;

            if (parameters.Count > 0)
            {
                query = $"&{string.Join("&", parameters.ToArray())}";
            }

            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"UserAccount/SendRegistrationCode?verificationID={verificationId}&isPatient=true&sendMethod={sendMethod.ToString().ToLower()}{query}", null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> AuthenticateRegistrationCodeAsync(string sApiDomainURL, int codeId, string code)
        {
            return await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"UserAccount/AuthenticateRegistrationCode?codeID={codeId}&code={code}", null, null, RestSharp.Method.Get);
        }

        public static async Task<StatusResponse> UpdateMedicalHistoryAsync(string sApiDomainURL, MedicalInfo medicalInfo, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "Patient/UpdateMedicalHistory", token, medicalInfo);
            return resp;
        }

        public static async Task<PatientsForVisit> PatientStartVisitStep1Async(string sApiDomainURL, int loginId, string token)
        {
            PatientsForVisit resp = await ApiUtility.SendApiRequestAsync<PatientsForVisit>(sApiDomainURL, $"Visits/PatientStartVisitStep1?LoginID={loginId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<MedicalInfo> PatientStartVisitStep2Async(string sApiDomainURL, int patientId, string token)
        {
            MedicalInfo resp = await ApiUtility.SendApiRequestAsync<MedicalInfo>(sApiDomainURL, $"Visits/PatientStartVisitStep2?PatientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<List<ProviderInfo>> PatientStartVisitStep3Async(string sApiDomainURL, int patientId, string token)
        {
            List<ProviderInfo> resp = await ApiUtility.SendApiRequestAsync<List<ProviderInfo>>(sApiDomainURL, $"Visits/PatientStartVisitStep3?PatientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<List<GenericRecord>> PatientStartVisitStep4Async(string sApiDomainURL, string token)
        {
            List<GenericRecord> resp = await ApiUtility.SendApiRequestAsync<List<GenericRecord>>(sApiDomainURL, "Visits/PatientStartVisitStep4", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<VisitInfo> PatientResumeVisitAsync(string sApiDomainURL, int VisitID, string token)
        {
            VisitInfo resp = await ApiUtility.SendApiRequestAsync<VisitInfo>(sApiDomainURL, $"Visits/PatientRestartVisit?VisitID={VisitID}", token, null, RestSharp.Method.Get);
            return resp;
        }
        public static async Task<VisitDetailsResponse> PatientStartVisitStep5Async(string sApiDomainURL, StartVisit startVisit, string token)
        {
            VisitDetailsResponse resp = await ApiUtility.SendApiRequestPostAsync<VisitDetailsResponse>(sApiDomainURL, "Visits/PatientStartVisitStep5", token, startVisit);
            return resp;
        }

        //new model for visit
        public static async Task<VisitDetailsResponse> NewPatientStartVisitStep5Async(string sApiDomainURL, StartVisitState startVisit, string token)
        {
            VisitDetailsResponse resp = await ApiUtility.SendApiRequestPostAsync<VisitDetailsResponse>(sApiDomainURL, "Visits/PatientStartVisitStep5", token, startVisit);
            return resp;
        }

        public static async Task<StatusResponse> ProviderEndVisitAsync(string sApiDomainURL, string visitID, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Visits/ProviderEndVisit?VisitID={visitID}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> UpdatePatientProfileAsync(string sApiDomainURL, string token, PatientProfile patientProfile)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "/Patient/UpdateProfile", token, patientProfile);
            return resp;
        }

        public static async Task<Registration> PatientStartPrePayRegistrationAsync(string sApiDomainURL, Guid token)
        {
            Registration resp = await ApiUtility.SendApiRequestAsync<Registration>(sApiDomainURL, $"UserAccount/StartPrePayRegistration?token={token}", null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<RegistrationState> _PatientStartPrePayRegistrationAsync(string sApiDomainURL, Guid token)
        {
            RegistrationState resp = await ApiUtility.SendApiRequestAsync<RegistrationState>(sApiDomainURL, $"UserAccount/StartPrePayRegistration?token={token}", null, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<AccountCreditCard> PatientGetCreditCardInfoAsync(string sApiDomainURL, int patientId, string token)
        {
            AccountCreditCard resp = await ApiUtility.SendApiRequestAsync<AccountCreditCard>(sApiDomainURL, $"Patient/GetCreditCardInfo?patientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> PatientUpdateCreditCardInfoAsync(string sApiDomainURL, string token, AccountCreditCard card)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "Patient/UpdateCreditCardInfo", token, card);
            return resp;
        }

        public static async Task<StatusResponse> PatientUpdatePasswordAsync(string sApiDomainURL, string token, Password password)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "Patient/UpdatePassword", token, password);
            return resp;
        }

        public static async Task<AccountSubscriptionInfo> PatientGetSubscriptionInfoAsync(string sApiDomainURL, int patientId, string token)
        {
            AccountSubscriptionInfo resp = await ApiUtility.SendApiRequestAsync<AccountSubscriptionInfo>(sApiDomainURL, $"Patient/GetSubscriptionInfo?patientID={patientId}", token, null, RestSharp.Method.Get);

            List<SubscriptionBase> availableSubscriptions = new List<SubscriptionBase>();
            if (resp != null)
            {
                foreach (SubscriptionBase sb in resp.AvailableSubscriptions.Where(x => x.IsAddOn == false))
                {
                    var subscription = SubscriptionsFactory.Get(sb.OptionID);
                    subscription.OptionID = sb.OptionID;
                    subscription.Cost = sb.Cost;
                    subscription.HasAddOn = sb.HasAddOn;
                    subscription.IsActive = sb.IsActive;
                    subscription.IsAddOn = sb.IsAddOn;
                    subscription.MonthlyVisits = sb.MonthlyVisits;
                    subscription.Name = sb.Name;
                    subscription.PlanDescription = sb.PlanDescription;
                    subscription.TotalOptionMembers = sb.TotalOptionMembers;
                    subscription.GrantedPromotionCode = sb.GrantedPromotionCode;
                    subscription.GrantedPromotionDescrip = sb.GrantedPromotionDescrip;
                    subscription.GrantedPromotionID = sb.GrantedPromotionID;

                    if (subscription is FamilySubscription || subscription is Family365Subscription)
                    {
                        var addon = resp.AvailableSubscriptionAddOns[0];
                        var addonsubscription = SubscriptionsFactory.Get(addon.OptionID);
                        addonsubscription.OptionID = addon.OptionID;
                        addonsubscription.Cost = addon.Cost;
                        addonsubscription.HasAddOn = addon.HasAddOn;
                        addonsubscription.IsActive = addon.IsActive;
                        addonsubscription.IsAddOn = addon.IsAddOn;
                        addonsubscription.MonthlyVisits = addon.MonthlyVisits;
                        addonsubscription.Name = addon.Name;
                        addonsubscription.PlanDescription = addon.PlanDescription;
                        addonsubscription.TotalOptionMembers = addon.TotalOptionMembers;
                        if (subscription is Family365Subscription) ((Family365Subscription)subscription).AddOn = (AdditionalFamilyMemberSubscription)addonsubscription;
                        else
                            ((FamilySubscription)subscription).AddOn = (AdditionalFamilyMemberSubscription)addonsubscription;
                    }
                    availableSubscriptions.Add(subscription);
                }

                resp.AvailableSubscriptions = availableSubscriptions;
            }
            return resp;
        }

        public static async Task<StatusResponse> PatientGetCancelSubscriptionDateAsync(string sApiDomainURL, int patientId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/GetCancelSubscriptionDate?patientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> PatientCancelSubscriptionAsync(string sApiDomainURL, int patientId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/CancelSubscription?patientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<SubscriptionChangeInfo> PatientGetChangeSubscriptionInfoAsync(string sApiDomainURL, int patientId, int newSubscriptionOptionId, string token, string promoCode = null)
        {
            SubscriptionChangeInfo resp = await ApiUtility.SendApiRequestAsync<SubscriptionChangeInfo>(sApiDomainURL, $"Patient/GetChangeSubscriptionInfo?patientID={patientId}&newSubscriptionOptionID={newSubscriptionOptionId}&promoCode={promoCode}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> PatientChangeSubscriptionAsync(string sApiDomainURL, AccountSubscriptionChange accountSubscriptionChange, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "Patient/ChangeSubscription", token, accountSubscriptionChange);
            return resp;
        }

        public static async Task<List<BasicFamilyMemberInfo>> PatientGetFamilyMemberListAsync(string sApiDomainURL, int patientId, string token)
        {
            List<BasicFamilyMemberInfo> resp = await ApiUtility.SendApiRequestAsync<List<BasicFamilyMemberInfo>>(sApiDomainURL, $"Patient/GetFamilyMemberList?patientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<AccountAddFamilyMemberInfo> PatientGetAddFamilyMemberInfoAsync(string sApiDomainURL, int primaryPatientId, string token, int? numberOfFamilyMembers = null)
        {
            if (numberOfFamilyMembers.HasValue)
            {
                return await ApiUtility.SendApiRequestAsync<AccountAddFamilyMemberInfo>(sApiDomainURL, $"Patient/GetAddFamilyMemberInfo?primaryPatientID={primaryPatientId}&numberOfFamilyMembers={numberOfFamilyMembers}", token, null, RestSharp.Method.Get);
            }
            else
            {
                return await ApiUtility.SendApiRequestAsync<AccountAddFamilyMemberInfo>(sApiDomainURL, $"Patient/GetAddFamilyMemberInfo?primaryPatientID={primaryPatientId}", token, null, RestSharp.Method.Get);
            }
        }

        public static async Task<StatusResponse> PatientAddFamilyMembersAsync(string sApiDomainURL, AccountAddFamilyMember members, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "Patient/AddFamilyMembers", token, members);
            return resp;
        }

        public static async Task<StatusResponse> _PatientAddFamilyMembersAsync(string sApiDomainURL, AccountAddFamilyMemberState members, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "Patient/AddFamilyMembers", token, members);
            return resp;
        }

        public static async Task<StatusResponse> PatientMakeFamilyMemberPrivateAsync(string sApiDomainURL, int primaryPatientId, int familyMemberPatientId, string familyMemberEmail, string token, string optionalEmailForTesting = null)
        {
            string optionalParam = string.Empty;
            if (!string.IsNullOrEmpty(optionalEmailForTesting))
            {
                optionalParam = $"&optionalEmailForTesting={optionalEmailForTesting}";
            }

            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/MakeFamilyMemberPrivate?primaryPatientID={primaryPatientId}&familyMemberPatientID={familyMemberPatientId}&familyMemberEmail={familyMemberEmail}{optionalParam}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> RemovePrivateFamilyMemberInfoAsync(string sApiDomainURL, int familyMemberPatientId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/RemovePrivateFamilyMemberInfo?familyMemberPatientID={familyMemberPatientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        //
        public static async Task<StatusResponse> GetActiveCoverageAsync(string sApiDomainURL, int patientId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"/Patient/GetActiveCoverage?patientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }
        //Patient/GetActiveCoverage

        public static async Task<StatusResponse> RemoveFamilyMemberAsync(string sApiDomainURL, int primaryPatientId, int familyMemberPatientId, string email, bool newEmail, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/RemoveFamilyMember?primaryPatientID={primaryPatientId}&familyMemberPatientID={familyMemberPatientId}&email={email}&newEmail={newEmail}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> DeactivateFamilyMemberAsync(string sApiDomainURL, int primaryPatientId, int familyMemberPatientId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/DeactivateFamilyMember?primaryPatientID={primaryPatientId}&familyMemberPatientID={familyMemberPatientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<string> GetProviderResponseTimeAsync(string sApiDomainURL, int providerId, DateTime startDate, DateTime endDate, string token)
        {
            string resp = await ApiUtility.SendApiRequestAsync<string>(sApiDomainURL, $"Provider/ResponseTime?providerID={providerId}&startDate={startDate}&endDate={endDate}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<int> GetProviderPatientVisitCountAsync(string sApiDomainURL, int providerId, DateTime startDate, DateTime endDate, string token)
        {
            int resp = await ApiUtility.SendApiRequestAsync<int>(sApiDomainURL, $"Provider/PatientVisits?providerID={providerId}&startDate={startDate}&endDate={endDate}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<List<Addendum>> GetVisitAddendaListAsync(string sApiDomainURL, string sVisitID, string token)
        {
            List<Addendum> resp = await ApiUtility.SendApiRequestAsync<List<Addendum>>(sApiDomainURL, $"/Visits/GetVisitAddenda?visitID={sVisitID}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderVisits> GetPatientAccountVisitsAsync(string sApiDomainURL, int patientId, string token, int? startIndex = null, int? endIndex = null)
        {
            string query = string.Empty;
            if (startIndex != null && endIndex != null)
            {
                query = string.Concat(query, string.Format("&takeAll=false&startIndex={0}&endIndex={1}", startIndex, endIndex));
            }
            ProviderVisits resp = await ApiUtility.SendApiRequestAsync<ProviderVisits>(sApiDomainURL, $"/Visits/GetPatientAccountVisitsMobile?primaryPatientID={patientId}&IsPrivate=false{query}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderVisits> GetPatientVisitsAsync(string sApiDomainURL, int patientId, string token, int? startIndex = null, int? endIndex = null)
        {
            string query = string.Empty;
            if (startIndex != null && endIndex != null)
            {
                query = string.Concat(query, string.Format("&takeAll=false&startIndex={0}&endIndex={1}", startIndex, endIndex));
            }
            ProviderVisits resp = await ApiUtility.SendApiRequestAsync<ProviderVisits>(sApiDomainURL, $"/Visits/PatientVisitsMobile?patientID={patientId}&IsPrivate=false{query}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<ProviderVisits> GetPatientLastTwoVisitsAsync(string sApiDomainURL, int patientId, string token, int? startIndex = null, int? endIndex = null)
        {
            ProviderVisits resp = await ApiUtility.SendApiRequestAsync<ProviderVisits>(sApiDomainURL, $"/Visits/PatientLastTwoVisits?patientID={patientId}", token, null, RestSharp.Method.Get);
            return resp;
        }
        //  let url = environment.API_URL + '/api/UserAccount/SendEmail';
        public static async Task<StatusResponse> SendSurveyEmail(string sApiDomainURL, string link, string token, string visitID)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"/UserAccount/SendEmail?visitID={visitID}&link={link}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<PrivateLinkInfo> GetMakePrivateLinkInfoAsync(string sApiDomainURL, string token, string passwordId)
        {
            PrivateLinkInfo resp = await ApiUtility.SendApiRequestAsync<PrivateLinkInfo>(sApiDomainURL, $"/UserAccount/GetMakePrivateLinkInfo?passwordID={passwordId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> CreatePrivateMemberPasswordAsync(string sApiDomainURL, PrivateMemberPassword privateMemberPassword, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, "UserAccount/CreatePrivateMemberPassword", token, privateMemberPassword);
            return resp;
        }

        public static async Task<StatusResponse> ReactivateFamilyMemberInfoAsync(string sApiDomainURL, int primaryPatientId, int familyMemberPatientId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/ReactivateFamilyMemberInfo?primaryPatientID={primaryPatientId}&familyMemberPatientID={familyMemberPatientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> ReactivateFamilyMemberAsync(string sApiDomainURL, int primaryPatientId, int familyMemberPatientId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Patient/ReactivateFamilyMember?primaryPatientID={primaryPatientId}&familyMemberPatientID={familyMemberPatientId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> StoreDeviceIdAsync(string sApiDomainURL, string deviceId, string deviceType, int userId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Messaging/StoreDeviceID?userID={userId}&deviceID={deviceId}&deviceType={deviceType}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> ClearDeviceBadgeCountAsync(string sApiDomainURL, string deviceId, string deviceType, int userId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL,
                $"Messaging/ClearDeviceBadgeCount?deviceID={deviceId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> GetRemainingVisitCountAsync(string sApiDomainURL, int loginId, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Visits/GetRemainingVisitCount?loginID={loginId}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> PollVisitStatusAsync(string sApiDomainURL, int visitId, string token)
        {
            ApiUtility.RaiseProgressEvents = false;
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"Visits/PollVisitStatus?VisitID={visitId}", token, null, RestSharp.Method.Get);
            ApiUtility.RaiseProgressEvents = true;
            return resp;
        }

        public static async Task<StatusResponse> UploadProviderImageAsync(string sApiDomainURL, string token, byte[] imageBytes)
        {
            ApiUtility.RaiseProgressEvents = false;
            StatusResponse resp = await ApiUtility.UploadImageAsync(sApiDomainURL, $"Provider/UpdatePhoto?providerID={Globals.Instance.UserInfo.ProviderID}", token, RestSharp.Method.Post, imageBytes);
            ApiUtility.RaiseProgressEvents = true;
            return resp;
        }

        public static async Task<StatusResponse> UploadPatientImageAsync(string sApiDomainURL, int patientId, string token, byte[] imageBytes)
        {
            ApiUtility.RaiseProgressEvents = false;
            StatusResponse resp = await ApiUtility.UploadImageAsync(sApiDomainURL, $"Patient/UpdatePhoto?patientID={patientId}", token, RestSharp.Method.Post, imageBytes);
            ApiUtility.RaiseProgressEvents = true;
            return resp;
        }

        public static async Task<StatusResponse> DeleteVisitAsync(string sApiDomainURL, int visitId, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"/Visits/DeleteVisit?visitID={visitId}", sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<int> RestartVisitAsync(string sApiDomainURL, int providerId, string sToken)
        {
            ApiUtility.RaiseProgressEvents = false;
            int resp = await ApiUtility.SendApiRequestAsync<int>(sApiDomainURL, $"/Visits/RestartVisit?providerID={providerId}", sToken, null, RestSharp.Method.Get);
            ApiUtility.RaiseProgressEvents = true;
            return resp;
        }

        public static async Task<StatusResponse> SetRestartedVisitFlagAsync(string sApiDomainURL, int providerId, string sToken)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"/Visits/SetRestartedVisitFlag?providerID={providerId}", sToken, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> GetLastOrderSummaryAsync(string sApiDomainURL, int patientId, string sToken)
        {
            return await ApiUtility.SendApiRequestAsync<StatusResponse>(sApiDomainURL, $"/Patient/GetLastOrderSummary?patientID={patientId}", sToken, null, RestSharp.Method.Get);
        }

        public static async Task<byte[]> _GetLastOrderSummaryAsync(string sApiDomainURL, int patientId, string sToken)
        {
            return await ApiUtility.SendApiRequestAsync<byte[]>(sApiDomainURL, $"/Patient/GetLastOrderSummary?patientID={patientId}", sToken, null, RestSharp.Method.Get);
        }

        public static async Task<SubscriptionChangeInfo> GetChangeToPreviousSubscriptionInfoAsync(string sApiDomainURL, int patientId, string sToken)
        {
            return await ApiUtility.SendApiRequestAsync<SubscriptionChangeInfo>(sApiDomainURL, $"/Patient/GetChangeToPreviousSubscriptionInfo?patientID={patientId}", sToken, null, RestSharp.Method.Get);
        }

        public static async Task<byte[]> GetNewOrderSummaryAsync(string sApiDomainURL, int patientId, int newSubscriptionOptionId, string sToken, string promoCode)
        {
            return await ApiUtility.SendApiRequestAsync<byte[]>(sApiDomainURL, $"/Patient/GetNewOrderSummary?patientID={patientId}&newSubscriptionOptionID={newSubscriptionOptionId}&PromoCode={promoCode}", sToken, null, RestSharp.Method.Get);
        }

        public static async Task<byte[]> _GetAddNewFamilyMemberOrderSummaryAsync(string sApiDomainURL, AccountAddFamilyMemberState members, string token)
        {
            return await ApiUtility.SendApiRequestPostAsync<byte[]>(sApiDomainURL, "/Patient/GetAddNewFamilyMemberOrderSummary", token, members);
        }

        public static async Task<byte[]> GetAddNewFamilyMemberOrderSummaryAsync(string sApiDomainURL, AccountAddFamilyMember members, string token)
        {
            return await ApiUtility.SendApiRequestPostAsync<byte[]>(sApiDomainURL, "Patient/GetAddNewFamilyMemberOrderSummary", token, members);
        }

        public static async Task<bool> CheckIfLatestAppVersionAsync(string sApiDomainURL, string appVersion, string appName = "Android Patient")
        {
            return await ApiUtility.SendApiRequestAsync<bool>(sApiDomainURL, $"Helper/CheckIfLatestAppVersion?AppName={appName}&AppVersion={appVersion}", null, null, RestSharp.Method.Get);
        }
        //IsDistinctAppVersionRecommended
        public static async Task<bool> IsAppVersionLessThanRecommendation(string sApiDomainURL, string appVersion, string appName = "Patient")
        {
            return await ApiUtility.SendApiRequestAsync<bool>(sApiDomainURL, $"Helper/IsAppVersionLessThanRecommendation?AppName={appName}&AppVersion={appVersion}", null, null, RestSharp.Method.Get);
        }
        public static async Task<bool> GetZipCapsuleEligibilityAsync(string sApiDomainURL, string zip, string token)
        {
            return await ApiUtility.SendApiRequestAsync<bool>(sApiDomainURL, $"Patient/GetZipCapsuleEligibility?zip={zip}", token, null, RestSharp.Method.Get);
        }

        public static async Task<bool> GetCapsuleEligibilityForHomeViewDialogAsync(string sApiDomainURL, int patientID, string token)
        {
            return await ApiUtility.SendApiRequestAsync<bool>(sApiDomainURL, $"Patient/GetCapsuleEligibilityForHomeViewDialog?patientID={patientID}", token, null, RestSharp.Method.Get);
        }

        public static async Task<StatusResponse> UpdateCapsuleEligibilityForHomeViewDialogAsync(string sApiDomainURL, int patientID, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, $"Patient/UpdateCapsuleEligibilityForHomeViewDialog?patientID={patientID}", token, null);
            return resp;
        }

        public static async Task<CurativeCheckDTO> GetCurativeEligibilityForHomeViewDialogAsync(string sApiDomainURL, int patientID, string token)
        {
            CurativeCheckDTO resp = await ApiUtility.SendApiRequestAsync<CurativeCheckDTO>(sApiDomainURL, $"Patient/GetCurativeEligibilityForHomeViewDialog?patientID={patientID}", token, null, RestSharp.Method.Get);
            return resp;
        }

        public static async Task<StatusResponse> UpdateCurativeEligibilityForHomeViewDialogAsync(string sApiDomainURL, int patientID, bool homeDialogVisible, string token)
        {
            StatusResponse resp = await ApiUtility.SendApiRequestPostAsync<StatusResponse>(sApiDomainURL, $"Patient/UpdateCurativeEligibilityForHomeViewDialog?patientID={patientID}&homeDialogVisible={homeDialogVisible}", token, null);
            return resp;
        }
    }

    public enum SendMethod
    {
        Email,
        Phone
    }
}
