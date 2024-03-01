using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class ChangePasswordSuccessPage : ContentPage
{
    //To define the class lavel variable.
    ChangePasswordSuccessPageViewModel VM;
    #region Constructor
    public ChangePasswordSuccessPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new ChangePasswordSuccessPageViewModel(this.Navigation);
        }
        catch (Exception)
        {
        }
    }
    #endregion
}