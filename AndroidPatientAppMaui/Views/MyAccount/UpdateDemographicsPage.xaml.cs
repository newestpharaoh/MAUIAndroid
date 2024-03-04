using AndroidPatientAppMaui.ViewModels.MyAccount;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateDemographicsPage : ContentPage
{
    //To define the class lavel variable.
    UpdateDemographicsPageViewModel VM;

    #region Constructor
    public UpdateDemographicsPage(BasicFamilyMemberInfo bfmi)
	{
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new UpdateDemographicsPageViewModel(this.Navigation);
            VM.member = bfmi;
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            VM.UserName = Helpers.AppGlobalConstants.userInfo.Name;
            await VM.GetUpdateDemographics();
           
        }
        catch (Exception ex)
        { 
        }
    }
    #endregion


    private void spnrTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.Title = string.Empty;
            if (VM.patientProfile != null)
            {
                var item =sender as Picker; 
                VM.patientProfile.Title = item.SelectedItem.ToString();
            }
        }
        catch (Exception ex)
        { 
        }
    }

    private void spnrGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.spnrGender = string.Empty;
            if (VM.patientProfile != null)
            {
                //Picker picker = (Picker)sender;
                //string selectedGender = (string)picker.SelectedItem;
                var item = sender as Picker;
                VM.patientProfile.Gender = item.SelectedItem.ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void spnrRelationship_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.spnrRelationship = string.Empty;
            //Picker picker = (Picker)sender;
            //string selectedRelationship = (string)spnrRelationship.SelectedItem;
            var item = sender as Picker;
            string selectedRelationship = item.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(selectedRelationship) && selectedRelationship.IndexOf("box below", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                VM.lytOtherRelationship = true;
            }
            else
            {
                VM.lytOtherRelationship = false;
                // Clear the text in the other relationship entry if necessary
            }

            VM.patientProfile.Relationship = selectedRelationship;
        }
        catch (Exception ex)
        {
        }
    }

    private void spnrLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.spnrLanguage = string.Empty; 
            var item = sender as Picker;
            string selectedLanguage = item.SelectedItem.ToString();

            if (selectedLanguage == "English")
                VM.patientProfile.LanguageID = 1;
            else if (selectedLanguage == "Spanish")
                VM.patientProfile.LanguageID = 2;

            VM.patientProfile.Language = selectedLanguage;
        }
        catch (Exception ex)
        {
        }
    }

    private void spnrState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.spnrState = string.Empty;
            if (VM.patientProfile != null)
            {
                var item = sender as Picker;
                VM.patientProfile.State = item.SelectedItem.ToString();
            }
        }
        catch (Exception ex)
        { 
        }
    }
}