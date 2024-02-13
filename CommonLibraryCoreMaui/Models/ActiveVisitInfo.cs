using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraryCoreMaui.Models
{
    public class ActiveVisitInfo : ResponseBase
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<ActiveVisit> ActiveVisits = new List<ActiveVisit>();
    }

    public class VisitInfo : ResponseBase
    {
        public int PatientID { get; set; }
        public string StatusCode { get; set; }
        public string VisitReason { get; set; }
        public string Message { get; set; }
        public string Payload { get; set; }
    }
}
