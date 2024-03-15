using AndroidPatientAppMaui.ViewModels.MyAccount;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class AccountProfilePage : ContentPage
{ //To define the class lavel variable.
    AccountProfilePageViewModel VM;

    #region Constructor
    public AccountProfilePage()
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new AccountProfilePageViewModel(this.Navigation);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler

    /// <summary>
    /// TODO : To Define the on Appearing....
    /// </summary>
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            // updateGrid.IsVisible = false;
            //  arrowimg.Source = "rightarrow.png";

            await VM.GetAccountMembers();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO: To Define DownArrow Tapped
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DownArrow_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var result = (sender as Grid).BindingContext as AccountMember;
            result.IsExpanded = !result.IsExpanded;
            if (result.ImgArrow == "rightarrow.png")
            {
                result.ImgArrow = "downarrow.png";
            }
            else
            {
                result.ImgArrow = "rightarrow.png";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    private async void UpdateDemographics_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var result = (sender as Grid).BindingContext as AccountMember;
            VM.am = VM.info.AccountMembers.FirstOrDefault(x => x.PatientID == result.PatientID);
            if (VM.am != null)
            {
                BasicFamilyMemberInfo bfmi = new BasicFamilyMemberInfo();
                bfmi.DisplayName = VM.am.DisplayName;
                bfmi.DOB = VM.am.DOB;
                bfmi.FirstName = VM.am.FirstName;
                bfmi.IsActive = VM.am.IsActive;
                bfmi.IsPrimary = VM.am.IsPrimary;
                bfmi.LastName = VM.am.LastName;
                bfmi.PatientID = VM.am.PatientID;
                await Navigation.PushModalAsync(new Views.MyAccount.UpdateDemographicsPage(bfmi), false);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private async void UpdateMedicalInformation_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var result = (sender as Grid).BindingContext as AccountMember;
            VM.am = VM.info.AccountMembers.FirstOrDefault(x => x.PatientID == result.PatientID);
            if (VM.am != null)
            {
                if (VM.am.IsPrivate)
                { 
                    BasicFamilyMemberInfo bfmi = new BasicFamilyMemberInfo();
                    bfmi.DisplayName = VM.am.DisplayName; 
                    await Navigation.PushModalAsync(new Views.Family.FamilyMemberPrivateMedicalInformation(bfmi), false);
                }
                else
                {
                    //intent = new Intent(this, typeof(PatientPreVisitMedicalHistoryActivity));
                    //intent.PutExtra("nonvisit", true);
                    //intent.PutExtra("family", !am.IsPrimary);
                    //intent.PutExtra("PatientId", selectedPatientId);
                    //intent.PutExtra("PatientName", am.DisplayName);
                    App.Current.MainPage = new Views.MyMedicalInfo.MyMedicalInfoPage(VM.am);

                } 
            }
        }
        catch (Exception ex)
        {
        }
    }
    private async void UpdateAccountAccess_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var result = (sender as Grid).BindingContext as AccountMember;
            VM.am = VM.info.AccountMembers.FirstOrDefault(x => x.PatientID == result.PatientID); 
            if (VM.am != null)
            {
                await Navigation.PushModalAsync(new Views.MyAccount.UpdateAccountAccessPage(VM.am), false);
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion


}