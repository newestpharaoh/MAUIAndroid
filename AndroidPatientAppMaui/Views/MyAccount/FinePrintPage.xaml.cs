using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class FinePrintPage : ContentPage
{
    //To define the class lavel variable.
    FinePrintPageViewModel VM;

    #region Constructor
    public FinePrintPage()
	{
		try
		{
			InitializeComponent();
			this.BindingContext = VM = new FinePrintPageViewModel(this.Navigation);
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