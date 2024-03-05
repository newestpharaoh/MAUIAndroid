using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class ChangePasswordPage : ContentPage
{
    //To define the class lavel variable.
    ChangePasswordPageViewModel VM;
    #region Constructor
    public ChangePasswordPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new ChangePasswordPageViewModel(this.Navigation);
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