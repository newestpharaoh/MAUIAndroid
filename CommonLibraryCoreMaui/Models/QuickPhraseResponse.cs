using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models
{
    public class QuickPhrase
    {
        public int ProviderID { get; set; }
        public int PhraseID { get; set; }
        public string Phrase { get; set; }
        public int SortOrder { get; set; }
        public bool IsDisplayed { get; set; }
        public string PhraseTitle { get; set; }
    }

    public class QuickPhraseListResponse : ResponseBase
    {
        public List<QuickPhrase> QuickPhraseList { get; set; }
    }
}
