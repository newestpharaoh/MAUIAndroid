using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class AccountProfilePage : ContentPage
{
    public AccountProfilePage()
    {
        //To define the class lavel variable.
        AccountProfilePageViewModel VM;

        #region Constructor
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new AccountProfilePageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Event Handler
    /// <summary>
    /// TODO: To Define DownArrow Tapped
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DownArrow_Tapped(object sender, TappedEventArgs e)
    {
        if (updateGrid.IsVisible)
        {
            updateGrid.IsVisible = false;
            arrowimg.Source = "rightarrow.png";
        }
        else
        {
            updateGrid.IsVisible = true;
            arrowimg.Source = "downarrow.png";
        }
    }
    #endregion
}