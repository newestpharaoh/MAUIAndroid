using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class MyAccountPage : ContentPage
{
    MyAccountPageViewModel VM;
    public MyAccountPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new MyAccountPageViewModel(this.Navigation);
        }
        catch (Exception)
        {
        }
    }

    #region Event Handler
    #endregion

}