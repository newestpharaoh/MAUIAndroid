namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
    public class AddendaNavigationParam : INavigationParam
    {
        public int AddendaId { get; set; }
		public string VisitId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
    }
}