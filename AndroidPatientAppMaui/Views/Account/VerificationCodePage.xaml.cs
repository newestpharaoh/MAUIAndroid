using AndroidPatientAppMaui.ViewModels.Account;

namespace AndroidPatientAppMaui.Views.Account;

public partial class VerificationCodePage : ContentPage
{
    VerificationCodePageViewModel VM;
    public VerificationCodePage()
    {
        InitializeComponent();
        this.BindingContext = VM = new VerificationCodePageViewModel(this.Navigation);
    }

    #region Event Handler
    #endregion

}