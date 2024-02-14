using AndroidPatientApp.ViewModels.MyAccount;

namespace AndroidPatientApp.Views.MyAccount;

public partial class MyAccountPage : ContentPage
{
    MyAccountPageViewModel VM;
    public MyAccountPage()
	{
		InitializeComponent();
        this.BindingContext = VM = new MyAccountPageViewModel(this.Navigation);
    }

    #region Event Handler
    #endregion

}