using AndroidPatientAppMaui.ViewModels.VisitHistory;

namespace AndroidPatientAppMaui.Views.VisitHistory;

public partial class VisitHistoryPage : ContentPage
{
    VisitHistoryPageViewModel VM;
    public VisitHistoryPage()
	{
		try
		{
			InitializeComponent();
			this.BindingContext = VM = new VisitHistoryPageViewModel(this.Navigation);
		}
		catch (Exception)
		{
		}
    }

    #region Event Handler
    #endregion
}
