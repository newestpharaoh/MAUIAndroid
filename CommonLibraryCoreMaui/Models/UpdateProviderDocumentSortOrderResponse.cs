using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class ProviderDocument
    {
        public int ProviderID { get; set; }
        public int ProviderDocumentID { get; set; }
        public string FileName { get; set; }
        public string FileLink { get; set; }
        public int SortOrder { get; set; }
    }

    public class ProviderDocumentsResponse : ResponseBase
    {
        public List<ProviderDocument> DocumentList { get; set; }
    }
}
