using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class StartVisit
    {
        public int? PatientID { get; set; }
        public int? PatientGuardianID { get; set; } = 0;
        public int? ProviderID { get; set; }
        public List<string> ReasonsForVisit { get; set; } = new List<string>();
        public string OtherReasonsForVisit { get; set; }
        [JsonIgnore]
        public string PatientName { get; set; }
        [JsonIgnore]
        public string ProviderName { get; set; }
        public string OtherGuardianName { get; set; }
        public string OtherGuardianRelationship { get; set; }
        [JsonIgnore]
        public int? VisitID { get; set; }
        [JsonIgnore]
        public string PatientFirstName { get; set; }
        [JsonIgnore]
        public string PatientLastName { get; set; }
        [JsonIgnore]
        public bool? VisitForMe { get; set; }
        public bool? IsResumeVisit { get; set; }
        private static StartVisit m_oInstance = null;
        private static readonly object m_oPadLock = new object();
        //
        public bool Prepay {get; set; }
        public string Domain {get; set; }

        public static StartVisit Instance
        {
            get
            {
                lock (m_oPadLock)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new StartVisit();
                    }
                    return m_oInstance;
                }
            }
        }

        public void Clear()
        {
            this.PatientID = (int?)null;
            this.PatientGuardianID = (int?)null;
            this.ProviderID = (int?)null;
            this.OtherReasonsForVisit = string.Empty;
            this.ReasonsForVisit = new List<string>();
            this.PatientName = string.Empty;
            this.PatientLastName = string.Empty;
            this.VisitID = (int?)null;
            this.PatientFirstName = string.Empty;
            this.OtherGuardianName = string.Empty;
            this.OtherGuardianRelationship = string.Empty;
            this.VisitForMe = (bool?)null;
            this.IsResumeVisit = (bool?)null;
        }

        public void ResetProviderAndReason()
        {
            this.ProviderID = null;
            this.OtherReasonsForVisit = string.Empty;
            this.ReasonsForVisit = new List<string>();
        }
    }

    public class StartVisitState
    {
        public int? PatientID { get; set; }
        public int? PatientGuardianID { get; set; } = 0;
        public int? ProviderID { get; set; }
        public System.Collections.Generic.List<string> ReasonsForVisit { get; set; } = new System.Collections.Generic.List<string>();
        public string OtherReasonsForVisit { get; set; }
        public string PatientName { get; set; }
        public string ProviderName { get; set; }
        public string OtherGuardianName { get; set; }
        public string OtherGuardianRelationship { get; set; }
        public int? VisitID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public bool? VisitForMe { get; set; }
        public bool? IsResumeVisit { get; set; }
        public bool Prepay {get; set; }
        public string Domain {get; set; }

    }
}
