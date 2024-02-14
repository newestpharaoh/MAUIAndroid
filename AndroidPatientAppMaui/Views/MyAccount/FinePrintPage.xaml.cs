using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class FinePrintPage : ContentPage
{
    FinePrintPageViewModel VM;
    public FinePrintPage()
	{
		try
		{
			InitializeComponent();
			this.BindingContext = VM = new FinePrintPageViewModel(this.Navigation);
		}
		catch (Exception ex)
		{			
		}
    }

    #region Event Handler
    #endregion

}