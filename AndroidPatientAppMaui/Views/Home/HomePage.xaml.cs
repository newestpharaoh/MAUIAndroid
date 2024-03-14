using AndroidPatientAppMaui.ViewModels.Home;
namespace AndroidPatientAppMaui.Views.Home;

public partial class HomePage : ContentPage
{
    HomePageViewModel VM;
    private string phonenumber;

    public HomePage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new HomePageViewModel(this.Navigation);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #region Event Handler
    /// <summary>
    /// TODO : To Define the ON Appearing....
    /// </summary>
    protected async override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetUserInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To Define the Phone Dialer Tapped event....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void PhoneDialer_Tapped(object sender, TappedEventArgs e) 
    {
        try
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Phone>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Phone>();
            }
            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open("512-421-5678");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion
}