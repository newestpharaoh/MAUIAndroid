using AndroidPatientAppMaui.ViewModels.MyAccount;
using CommonLibraryCoreMaui.Models; 

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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    /// <summary>
    /// TODO : To define the OnAppearing...
    /// </summary>
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.GetUpdateAccountAccessInfo();
            await VM.GetRemoveUserFromFamilyAccountDetails();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
  
    /// <summary>
    /// TODO : To define Opacity grid tapped...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void opacitygrid_Tapped(object sender, TappedEventArgs e)
    {
        try
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
            RemoveUserFromFamilyAccountDialogFragment.IsVisible = false;
            ReactivateUserAccountDialogFragment.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    } 
    /// <summary>
    /// TODO : To define the Privatize Info Tapped...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PRIVATIZEInfo_Tapped(object sender, TappedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define the Deactivate Info Tapped...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DEACTIVATEInfo_Tapped(object sender, TappedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To Define the Remove Info Tapped...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void REMOVEInfo_Tapped(object sender, TappedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To Define Reactivate Info Tapped....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void REACTIVATEInfo_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = true;
            ReactivateInfoDialogFragment.IsVisible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
     
    /// <summary>
    /// TODO : To Define No Button...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NoButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = false;
            PatientSettingsManageSubscriptionMemberPrivateEmail.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To Define Privatize User Button...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PrivatizeUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = true;
            PatientSettingsManageSubscriptionMemberPrivateEmail.IsVisible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define Deactivate Button...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeactivateButton_Clicked(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define Close Button...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = false;
            DeactivateAccountHolderDialogFragment.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To Define Cancel Button...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = false;
            DeactivateAccountHolderDialogFragment.IsVisible = false;
            RemoveUserFromFamilyAccountDialogFragment.IsVisible = false;
            ReactivateUserAccountDialogFragment.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : to define Remove User Button...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void RemoveUserButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            opacitygrid.IsVisible = true;
            RemoveUserFromFamilyAccountDialogFragment.IsVisible = true;
            await VM.GetRemoveUserButtonAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To Define Reactivate User Button...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ReactivateUserButton_Clicked(object sender, EventArgs e)
    { 
        try
        {
            await VM.GetReactivateUserButtonAsync();
            if (VM.resp.StatusCode != StatusCode.UserCantBeAdded)
            {
                opacitygrid.IsVisible = true;
                ReactivateUserAccountDialogFragment.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion
}