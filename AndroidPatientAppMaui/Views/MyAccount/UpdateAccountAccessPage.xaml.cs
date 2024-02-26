using AndroidPatientAppMaui.ViewModels.MyAccount;
using CommonLibraryCoreMaui.Models;
using FM.LiveSwitch.Dtmf;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateAccountAccessPage : ContentPage
{
    //To define the class lavel variable.
    UpdateAccountAccessPageViewModel VM;
    #region Constructor
    public UpdateAccountAccessPage(AccountMember am)
    {
        try
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var Width = (mainDisplayInfo.Width / mainDisplayInfo.Density) - 40;
            PrivatizeInfoAccountHolderDialogFragment.WidthRequest = Width; 
            PrivatizeInfoDialogFragment.WidthRequest = Width;
            DeactivateInfoAccountHolderDialogFragment.WidthRequest = Width;
            DeactivateInfoDialogFragment.WidthRequest = Width;
            RemoveInfoAccountHolderDialogFragment.WidthRequest = Width;
            RemoveInfoDialogFragment.WidthRequest = Width;
            ReactivateInfoDialogFragment.WidthRequest = Width;
            this.BindingContext = VM = new UpdateAccountAccessPageViewModel(this.Navigation);
            VM.am = am;
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region Event Handler
    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.GetUpdateAccountAccessInfo();
    }
    #endregion

    private void opacitygrid_Tapped(object sender, TappedEventArgs e)
    {
        opacitygrid.IsVisible = false;
        PrivatizeInfoAccountHolderDialogFragment.IsVisible = false;
        PrivatizeInfoDialogFragment.IsVisible = false;
        DeactivateInfoAccountHolderDialogFragment.IsVisible = false;
        DeactivateInfoDialogFragment.IsVisible = false;
        RemoveInfoAccountHolderDialogFragment.IsVisible = false;
        RemoveInfoDialogFragment.IsVisible = false;
        ReactivateInfoDialogFragment.IsVisible = false;
        PatientSettingsManageSubscriptionMemberPrivateEmail.IsVisible = false;
        DeactivateAccountHolderDialogFragment.IsVisible = false;
    }


    private void PRIVATIZEInfo_Tapped(object sender, TappedEventArgs e)
    {
        if (VM.am != null)
        {
            if (VM.am.IsPrimary)
            {
                opacitygrid.IsVisible = true;
                PrivatizeInfoAccountHolderDialogFragment.IsVisible = true;
            }
            else
            {
                opacitygrid.IsVisible = true;
                PrivatizeInfoDialogFragment.IsVisible = true;
            }
        }
       
    }

    private void DEACTIVATEInfo_Tapped(object sender, TappedEventArgs e)
    {
        if (VM.am != null)
        {
            if (VM.am.IsPrimary)
            {
                opacitygrid.IsVisible = true;
                DeactivateInfoAccountHolderDialogFragment.IsVisible = true;
            }
            else
            {
                opacitygrid.IsVisible = true;
                DeactivateInfoDialogFragment.IsVisible = true;
            }
        } 
    }

    private void REMOVEInfo_Tapped(object sender, TappedEventArgs e)
    {
        if (VM.am != null)
        {
            if (VM.am.IsPrimary)
            {
                opacitygrid.IsVisible = true;
                RemoveInfoAccountHolderDialogFragment.IsVisible = true;
            }
            else
            {
                opacitygrid.IsVisible = true;
                RemoveInfoDialogFragment.IsVisible = true;
            }
        }
     }

    private void REACTIVATEInfo_Tapped(object sender, TappedEventArgs e)
    {
        opacitygrid.IsVisible = true;
        ReactivateInfoDialogFragment.IsVisible = true;
    }
     

    private void NoButton_Clicked(object sender, EventArgs e)
    {
        opacitygrid.IsVisible = false;
        PatientSettingsManageSubscriptionMemberPrivateEmail.IsVisible = false;
    }

    private void PrivatizeUserButton_Clicked(object sender, EventArgs e)
    {
        opacitygrid.IsVisible = true;
        PatientSettingsManageSubscriptionMemberPrivateEmail.IsVisible = true;
    }

    private void DeactivateButton_Clicked(object sender, EventArgs e)
    {
        if (VM.am.IsPrimary)
        {
            opacitygrid.IsVisible = true;
            DeactivateAccountHolderDialogFragment.IsVisible = true;
            CloseButton.IsVisible = true;
            CancelButton.IsVisible = false;
            SubmitButton.IsVisible = false;

        }
        else
        {
            opacitygrid.IsVisible = true;
            DeactivateAccountHolderDialogFragment.IsVisible = true;
            CloseButton.IsVisible = false;
            CancelButton.IsVisible = true;
            SubmitButton.IsVisible = true;
        }
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        opacitygrid.IsVisible = false;
        DeactivateAccountHolderDialogFragment.IsVisible = false;
    }

    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        opacitygrid.IsVisible = false;
        DeactivateAccountHolderDialogFragment.IsVisible = false;
    }
}