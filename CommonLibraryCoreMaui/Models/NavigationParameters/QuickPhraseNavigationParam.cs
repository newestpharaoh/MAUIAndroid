namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
    public class QuickPhraseNavigationParam : INavigationParam
    {
        public int? PhraseID { get; set; }
        public string PhraseTitle { get; set; }
        public string PhraseDescription { get; set; }
    }

    public class VisitQuickPhraseNavigationParam : INavigationParam
    {
        public string VisitID { get; set; }
        public string PhraseText { get; set; }
    }

    public class VisitDocumentNavigationParam : INavigationParam
    {
        public string VisitID { get; set; }
        public string ProviderDocumentID { get; set; }
        public string ProviderDocumentName { get; set; }
    }
}