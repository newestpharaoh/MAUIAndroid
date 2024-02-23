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
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var Width = (mainDisplayInfo.Width / mainDisplayInfo.Density) - 40;
            InfoBorder.WidthRequest = Width;
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
            VM.GetBillingPoliciesInfo();
        }
        catch (Exception ex)
        {
             
        }
    }
    #endregion
  
    private void IconInfo_Tapped(object sender, TappedEventArgs e)
    {
        opacitygrid.IsVisible = true;
        InfoBorder.IsVisible = true;
    }
     
    private void opacitygrid_Tapped(object sender, TappedEventArgs e)
    {
        opacitygrid.IsVisible = false;
        InfoBorder.IsVisible = false;
    }
}