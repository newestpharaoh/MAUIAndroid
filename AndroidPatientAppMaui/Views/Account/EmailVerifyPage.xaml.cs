using AndroidPatientAppMaui.ViewModels.Account;

namespace AndroidPatientAppMaui.Views.Account;

public partial class EmailVerifyPage : ContentPage
{
    EmailVerifyPageViewModel VM;
    public EmailVerifyPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new EmailVerifyPageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #region Event Handler
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetUserContactAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion
}