using AndroidPatientAppMaui.ViewModels.Account;

namespace AndroidPatientAppMaui.Views;

public partial class ForgotPasswordPage : ContentPage
{
    //TODO : To Define Local Class Level Variables...
    ForgotPasswordPageViewModel VM;

    #region Constructor
    public ForgotPasswordPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new ForgotPasswordPageViewModel(this.Navigation);
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