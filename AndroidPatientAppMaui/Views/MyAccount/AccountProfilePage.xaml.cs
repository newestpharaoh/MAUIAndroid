using AndroidPatientAppMaui.ViewModels.MyAccount;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class AccountProfilePage : ContentPage
{ //To define the class lavel variable.
    AccountProfilePageViewModel VM;

    #region Constructor
    public AccountProfilePage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new AccountProfilePageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler

    /// <summary>
    /// TODO : To Define the on Appearing....
    /// </summary>
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            updateGrid.IsVisible = false;
          //  arrowimg.Source = "rightarrow.png";

            await VM.GetAccountMembers();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO: To Define DownArrow Tapped
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DownArrow_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            //if (updateGrid.IsVisible)
            //{
            //    updateGrid.IsVisible = false;
            //    arrowimg.Source = "rightarrow.png";
            //}
            //else
            //{
            //    updateGrid.IsVisible = true;
            //    arrowimg.Source = "downarrow.png";
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion
}