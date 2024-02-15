using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class BillingPoliciesPage : ContentPage
{
    BilingPoliciesPageViewModel VM;

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

    #region Event Handler
    #endregion
}