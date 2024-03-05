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
            Console.WriteLine(ex);
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
            Console.WriteLine(ex);
        }
    }
    #endregion
  
    private void IconInfo_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = true;
            InfoBorder.IsVisible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
     
    private void opacitygrid_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = false;
            InfoBorder.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}