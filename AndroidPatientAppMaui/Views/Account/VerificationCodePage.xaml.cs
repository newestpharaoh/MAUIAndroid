using AndroidPatientAppMaui.ViewModels.Account;

namespace AndroidPatientAppMaui.Views.Account;

public partial class VerificationCodePage : ContentPage
{
    VerificationCodePageViewModel VM;
    public VerificationCodePage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new VerificationCodePageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #region Event Handler
    #endregion

}