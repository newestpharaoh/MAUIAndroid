using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class TermsOfUsePage : ContentPage
{
    TermsOfUsePageViewModel VM;
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
    #region Event Handler
    #endregion
}