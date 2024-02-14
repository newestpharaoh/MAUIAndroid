using AndroidPatientAppMaui.ViewModels.VisitHistory;

namespace AndroidPatientAppMaui.Views.VisitHistory;

public partial class VisitHistoryPage : ContentPage
{
    VisitHistoryPageViewModel VM;
    public VisitHistoryPage()
	{
		InitializeComponent();
        this.BindingContext = VM = new VisitHistoryPageViewModel(this.Navigation);
    }


    #region Event Handler
    #endregion
}
