using System.Collections.Generic;

namespace CommonLibraryCoreMaui.Models.NavigationParameters
{
    public class VisitHistoryNavigationParam : INavigationParam
	{
		public VisitDetailsResponse VisitDetails { get; set; }
		public List<AbsenceNote> AbsenceNotes { get; set; }
	}
}
