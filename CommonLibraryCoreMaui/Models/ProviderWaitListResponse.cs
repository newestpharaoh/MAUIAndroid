using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class ProviderWaitListResponse
    {
        public int PatientWaitListCount { get; set; }
        public List<WaitingPatient> PatientWaitList { get; set; }
    }
}
