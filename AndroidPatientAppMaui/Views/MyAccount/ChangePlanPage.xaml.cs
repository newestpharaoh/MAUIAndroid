using AndroidPatientAppMaui.ViewModels.MyAccount;
using CommonLibraryCoreMaui.Models;
using DotLiquid.Util;
using FM.LiveSwitch;
using System.Runtime.CompilerServices;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class ChangePlanPage : ContentPage
{
    //To define the class lavel variable.
    ChangePlanPageViewModel VM;
    Subscription currentSubscription;
    #region Constructor
    public ChangePlanPage(AccountSubscriptionInfo info, bool isPlanChange)
    {
        InitializeComponent();
        this.BindingContext = VM = new ChangePlanPageViewModel(this.Navigation);
        VM.info = info;
        VM.planChange = isPlanChange;
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetChangePlanInfo();
            if (VM.PaymentPlansList != null)
            {
                //PaymentPlansList = new List<Subscription>() { new IndividualSubscription() { Name = info.CurrentSubscriptionPlan } };
                VM.PaymentPlansSelectedItems = VM.PaymentPlansList[0].Name;
                spnrPaymentPlans.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
             
        }

    }

    #endregion

    private void spnrPaymentPlans_SelectedIndexChanged(object sender, EventArgs e)
    {
        VM.selectedSubscription = (Subscription)spnrPaymentPlans.SelectedItem;

        if (VM.selectedSubscription != null)
        {
            // Update the plan description label
            if (!string.IsNullOrEmpty(VM.selectedSubscription.PlanDescription))
            {
                lblPlanDescription.Text = VM.selectedSubscription.PlanDescription.Replace("\\n", "\n");
            }

            // Adjust visibility based on conditions
            lytContinue.IsVisible = (VM.currentSubscription != null &&
                                      VM.selectedSubscription.OptionID.Equals(VM.currentSubscription.OptionID)) ? false : true;
        } 
    }
}