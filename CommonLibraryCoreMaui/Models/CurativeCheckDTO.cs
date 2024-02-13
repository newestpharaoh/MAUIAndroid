using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class CurativeCheckDTO
    {
        public bool CurativeEligibilityForHomeViewDialog { get; set; }
        public bool IsNewPatient { get; set; }
        public bool IsCurative { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}
