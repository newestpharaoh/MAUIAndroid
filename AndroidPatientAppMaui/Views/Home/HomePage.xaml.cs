using AndroidPatientAppMaui.ViewModels.Home;
namespace AndroidPatientAppMaui.Views.Home;

public partial class HomePage : ContentPage
{
    HomePageViewModel VM;
    private string phonenumber;

    public HomePage()
    {
        InitializeComponent();
        this.BindingContext = VM = new HomePageViewModel(this.Navigation);
    }

    #region Event Handler
    private async void PhoneDialer_Tapped(object sender, TappedEventArgs e)

    {
            var status = await Permissions.CheckStatusAsync<Permissions.Phone>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Phone>();
            }
            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open("512-421-5678");    
    }
    #endregion
}