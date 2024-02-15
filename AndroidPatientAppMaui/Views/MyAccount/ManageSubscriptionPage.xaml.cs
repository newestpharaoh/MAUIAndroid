using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class ManageSubscriptionPage : ContentPage
{
    ManageSubscriptionPageViewModel VM;

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
        #region Event Handler
        #endregion
    }