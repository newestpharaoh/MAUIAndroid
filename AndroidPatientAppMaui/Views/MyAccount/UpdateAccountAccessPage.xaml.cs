using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateAccountAccessPage : ContentPage
{
    //To define the class lavel variable.
    UpdateAccountAccessPageViewModel VM;
    #region Constructor
    public UpdateAccountAccessPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new UpdateAccountAccessPageViewModel(this.Navigation);
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region Event Handler
    #endregion
}