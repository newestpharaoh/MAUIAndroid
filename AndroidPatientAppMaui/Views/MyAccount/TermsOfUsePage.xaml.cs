using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class TermsOfUsePage : ContentPage
{
    //To define the class lavel variable.
    TermsOfUsePageViewModel VM;

    #region Constructor
    public TermsOfUsePage()
    {
		try
		{
			InitializeComponent();
            this.BindingContext = VM = new TermsOfUsePageViewModel(this.Navigation);
        }
		catch (Exception ex)
		{

		}
	}
    #endregion

    #region Event Handler
    #endregion
}