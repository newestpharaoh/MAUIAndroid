using AndroidPatientAppMaui.ViewModels.MyAccount;
using AndroidPatientAppMaui.ViewModels.Popup;
using AndroidPatientAppMaui.Views.MyAccount;

namespace AndroidPatientAppMaui.Views.Popup;

public partial class InfoPopupPage : CommunityToolkit.Maui.Views.Popup
{
    //To define the class lavel variable.
    InfoPopupPageViewModel VM;
    ManageSubscriptionPageViewModel ManageVM;
    public InfoPopupPage(ManageSubscriptionPageViewModel manageVM, ManageSubscriptionPage manageSubscriptionPage)
	{
        try
        {
		InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var Width = (mainDisplayInfo.Width / mainDisplayInfo.Density) - 50;
            InfoGrid.WidthRequest = Width;
            InfoBorder.WidthRequest = Width;
            //MonthlySubscriptionPlan.WidthRequest = Width;
            // BodyGrid.WidthRequest = Width;
            VM = new InfoPopupPageViewModel(null, this, manageSubscriptionPage);
            ManageVM = manageVM;
            this.BindingContext = VM;
            VM.GetBillingPoliciesInfo();

        }
        catch (Exception ex)
        {

        }
	}

    #region Event Handler

    #endregion
}