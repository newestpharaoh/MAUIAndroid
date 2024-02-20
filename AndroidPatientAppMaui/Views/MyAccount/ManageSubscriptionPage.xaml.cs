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
            this.BindingContext = VM = new ManageSubscriptionPageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.GetPatientSubscriptionInfo();
    }
    #endregion
}