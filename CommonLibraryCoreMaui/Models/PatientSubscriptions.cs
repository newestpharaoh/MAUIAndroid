using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibraryCoreMaui.Models
{
    public class PatientSubscriptions
    {
        public List<SubscriptionBase> AvailableSubscriptions { get; set; }
        public List<SubscriptionBase> AvailableSubscriptionAddOns { get; set; }

        public PatientSubscriptions()
        {
            AvailableSubscriptions = new List<SubscriptionBase>();
            AvailableSubscriptionAddOns = new List<SubscriptionBase>();
        }
    }

    public sealed class VisitsState
    {
        public void AddVisitState(int visitId, string encounterNotes, ICDCode primaryDiagnosis, ICDCode secondaryDiagnosis, string encounterType,
              bool? rx, bool lab, bool radiology, bool pcp, string specialistName, bool er, bool clinic, DateTime? telemed)
        {
            VisitState vs = visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                visits.Add(new VisitState()
                {
                    VisitId = visitId,
                    EncounterNotes = encounterNotes,
                    PrimaryDiagnosis = primaryDiagnosis,
                    SecondaryDiagnosis = secondaryDiagnosis,
                    EncounterType = encounterType,
                    Rx = rx,
                    Lab = lab,
                    Radiology = radiology,
                    PCP = pcp,
                    SpecialistName = specialistName,
                    ER = er,
                    Clinic = clinic,
                    Telemed = telemed
                });
            }
            else
            {
                vs.EncounterNotes = encounterNotes;
                vs.PrimaryDiagnosis = primaryDiagnosis;
                vs.SecondaryDiagnosis = secondaryDiagnosis;
                vs.EncounterType = encounterType;
                vs.Rx = rx;
                vs.Lab = lab;
                vs.Radiology = radiology;
                vs.PCP = pcp;
                vs.SpecialistName = specialistName;
                vs.ER = er;
                vs.Clinic = clinic;
                vs.Telemed = telemed;
            }
        }

        public void AddVisitAbsenceNoteState(int visitId, int? familyMemberId, string otherFamilyMember, string absenceNoteText, DateTime? returnToSchoolWork, string providerName)
        {
            VisitState vs = visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                visits.Add(new VisitState()
                {
                    VisitId = visitId,
                    ReturnToSchoolWork = returnToSchoolWork,
                    FamilyMemberId = familyMemberId,
                    OtherFamilyMember = otherFamilyMember,
                    AbsenceNoteText = absenceNoteText,
                    ProviderName = providerName
                });
            }
            else
            {
                vs.ReturnToSchoolWork = returnToSchoolWork;
                vs.FamilyMemberId = familyMemberId;
                vs.OtherFamilyMember = otherFamilyMember;
                vs.AbsenceNoteText = absenceNoteText;
                vs.ProviderName = providerName;
            }
        }

        public void AddVisitCurrentScreen(int visitId, VisitScreen visitScreen)
        {
            VisitState vs = visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                visits.Add(new VisitState()
                {
                    VisitId = visitId,
                    CurrentVisitScreen = visitScreen
                });
            }
            else
            {
                vs.CurrentVisitScreen = visitScreen;
            }
        }

        public void AddVisitAbsenceNoteId(int visitId, int? absenceNoteId)
        {
            VisitState vs = visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                visits.Add(new VisitState() { VisitId = visitId, AbsenceNoteId = absenceNoteId });
            }
            else
            {
                vs.AbsenceNoteId = absenceNoteId;
            }
        }

        public VisitState GetVisitState(int visitId)
        {
            return visits.Where(x => x.VisitId == visitId).FirstOrDefault();
        }

        private static VisitsState m_oInstance = null;
        private static readonly object m_oPadLock = new object();
        private List<VisitState> visits = new List<VisitState>();

        public static VisitsState Instance
        {
            get
            {
                lock (m_oPadLock)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new VisitsState();
                    }
                    return m_oInstance;
                }
            }
        }
    }

    public class VisitState
    {
        public int VisitId { get; set; }
        public string EncounterNotes { get; set; }
        public ICDCode PrimaryDiagnosis { get; set; }
        public ICDCode SecondaryDiagnosis { get; set; }
        public VisitScreen CurrentVisitScreen { get; set; }
        public string EncounterType { get; set; }
        public bool? Rx { get; set; } = null;
        public bool Lab { get; set; }
        public bool Radiology { get; set; }
        public bool PCP { get; set; }
        public string SpecialistName { get; set; }
        public bool ER { get; set; }
        public bool Clinic { get; set; }
        public DateTime? Telemed { get; set; }
        public int? AbsenceNoteId { get; set; }
        public string ProviderName { get; set; }
        public string AbsenceNoteText { get; set; }
        public DateTime? ReturnToSchoolWork { get; set; }
        public int? FamilyMemberId { get; set; }
        public string OtherFamilyMember { get; set; }
    }

    public enum VisitScreen
    {
        Chat = 0,
        VideoChat = 1,
        VoiceChat = 2,
        AbsenceNotes = 3,
        AbsenceNoteAddEdit = 4,
        EncounterNotes = 5,
        Diagnosis = 6,
        Confirmation = 7,
        Questionnaire = 8
    }

    [SharedPreferencesKey("visitsstate")]
    public sealed class VisitsStateEx : ISaveState
    {
        public void AddVisitState(int visitId, string encounterNotes, ICDCode primaryDiagnosis, ICDCode secondaryDiagnosis, string encounterType,
              bool? rx, bool lab, bool radiology, bool pcp, string specialistName, bool er, bool clinic, DateTime? telemed)
        {
            VisitState vs = Visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                Visits.Add(new VisitState()
                {
                    VisitId = visitId,
                    EncounterNotes = encounterNotes,
                    PrimaryDiagnosis = primaryDiagnosis,
                    SecondaryDiagnosis = secondaryDiagnosis,
                    EncounterType = encounterType,
                    Rx = rx,
                    Lab = lab,
                    Radiology = radiology,
                    PCP = pcp,
                    SpecialistName = specialistName,
                    ER = er,
                    Clinic = clinic,
                    Telemed = telemed
                });
            }
            else
            {
                vs.EncounterNotes = encounterNotes;
                vs.PrimaryDiagnosis = primaryDiagnosis;
                vs.SecondaryDiagnosis = secondaryDiagnosis;
                vs.EncounterType = encounterType;
                vs.Rx = rx;
                vs.Lab = lab;
                vs.Radiology = radiology;
                vs.PCP = pcp;
                vs.SpecialistName = specialistName;
                vs.ER = er;
                vs.Clinic = clinic;
                vs.Telemed = telemed;
            }
        }

        public void AddVisitAbsenceNoteState(int visitId, int? familyMemberId, string otherFamilyMember, string absenceNoteText, DateTime? returnToSchoolWork, string providerName)
        {
            VisitState vs = Visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                Visits.Add(new VisitState()
                {
                    VisitId = visitId,
                    ReturnToSchoolWork = returnToSchoolWork,
                    FamilyMemberId = familyMemberId,
                    OtherFamilyMember = otherFamilyMember,
                    AbsenceNoteText = absenceNoteText,
                    ProviderName = providerName
                });
            }
            else
            {
                vs.ReturnToSchoolWork = returnToSchoolWork;
                vs.FamilyMemberId = familyMemberId;
                vs.OtherFamilyMember = otherFamilyMember;
                vs.AbsenceNoteText = absenceNoteText;
                vs.ProviderName = providerName;
            }
        }

        public void AddVisitCurrentScreen(int visitId, VisitScreen visitScreen)
        {
            VisitState vs = Visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                Visits.Add(new VisitState()
                {
                    VisitId = visitId,
                    CurrentVisitScreen = visitScreen
                });
            }
            else
            {
                vs.CurrentVisitScreen = visitScreen;
            }
        }

        public void AddVisitAbsenceNoteId(int visitId, int? absenceNoteId)
        {
            VisitState vs = Visits.Where(x => x.VisitId == visitId).FirstOrDefault();
            if (vs is null)
            {
                Visits.Add(new VisitState() { VisitId = visitId, AbsenceNoteId = absenceNoteId });
            }
            else
            {
                vs.AbsenceNoteId = absenceNoteId;
            }
        }

        public VisitState GetVisitState(int visitId)
        {
            return Visits.Where(x => x.VisitId == visitId).FirstOrDefault();
        }

        public List<VisitState> Visits = new List<VisitState>();
    }
}
