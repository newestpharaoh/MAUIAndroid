using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class BillingPoliciesPage : ContentPage
{
    //To define the class lavel variable.
    BilingPoliciesPageViewModel VM;

    #region Constructor
    public BillingPoliciesPage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new BilingPoliciesPageViewModel(this.Navigation);
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
        await VM.GetBillingPoliciesInfo();
    }
    #endregion
}