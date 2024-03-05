using AndroidPatientAppMaui.ViewModels.VisitHistory;

namespace AndroidPatientAppMaui.Views.VisitHistory;

public partial class VisitHistoryPage : ContentPage
{
    //To define the class lavel variable.
    VisitHistoryPageViewModel VM;

    #region Constructor
    public VisitHistoryPage()
	{
		try
		{
			InitializeComponent();
			this.BindingContext = VM = new VisitHistoryPageViewModel(this.Navigation);
		}
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #endregion

    #region Event Handler
    #endregion
}
