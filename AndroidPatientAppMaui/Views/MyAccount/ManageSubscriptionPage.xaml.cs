using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class ManageSubscriptionPage : ContentPage
{
    //To define the class lavel variable.
    ManageSubscriptionPageViewModel VM;

    #region Constructor
    public ManageSubscriptionPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new ManageSubscriptionPageViewModel(this.Navigation, this);
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetPatientSubscriptionInfo();
        }
        catch (Exception ex)
        {
             
        }
    }
    #endregion
}