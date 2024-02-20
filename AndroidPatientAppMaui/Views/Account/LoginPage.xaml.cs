using AndroidPatientAppMaui.ViewModels.Account;
using CommonLibraryCoreMaui;
using CommonLibraryCoreMaui.Models;
using Microsoft.Maui;

namespace AndroidPatientAppMaui.Views;

public partial class LoginPage : ContentPage
{
    //TODO : To Define Local Class Level Variables...
    LoginPageViewModel VM;

    #region Constructor
    public LoginPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new LoginPageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #endregion

    #region Event Handler

    protected async override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetAppSettings();
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// TODO: To Define Website Tapped
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Website_click(object sender, TappedEventArgs e)
    {
        try
        {
            await Browser.OpenAsync(new Uri("https://care.normanmd.com"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
    #endregion

}