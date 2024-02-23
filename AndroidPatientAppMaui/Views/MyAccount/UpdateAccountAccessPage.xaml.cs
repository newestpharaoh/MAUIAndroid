using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateAccountAccessPage : ContentPage
{
    //To define the class lavel variable.
    UpdateAccountAccessPageViewModel VM;
    #region Constructor
    public UpdateAccountAccessPage()
    {
        try
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var Width = (mainDisplayInfo.Width / mainDisplayInfo.Density) - 40;
            privatizeBorder.WidthRequest = Width;
            this.BindingContext = VM = new UpdateAccountAccessPageViewModel(this.Navigation);
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region Event Handler
    #endregion

    private void opacitygrid_Tapped(object sender, TappedEventArgs e)
    {
        opacitygrid.IsVisible = false;
        privatizeBorder.IsVisible = false;
    }

    private void Info_Tapped(object sender, TappedEventArgs e)
    {
        opacitygrid.IsVisible = true;
        privatizeBorder.IsVisible = true;
    }
}