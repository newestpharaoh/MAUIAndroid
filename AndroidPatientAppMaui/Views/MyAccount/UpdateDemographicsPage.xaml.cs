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
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    /// <summary>
    /// TODO : To define the on appearing method...
    /// </summary>
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            //VM.UserName = Helpers.AppGlobalConstants.userInfo.Name;
            await VM.GetUpdateDemographics();
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    ///  TODO : To define the Title picker Sected Index Changed event... 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void spnrTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.Title = string.Empty; 
            if (VM.patientProfile != null)
            {
                var item = sender as Picker;
                VM.Title = item.SelectedItem.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    ///  TODO : To define the Gender picker Selecected change event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void spnrGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.spnrGender = string.Empty;
            if (VM.patientProfile != null)
            { 
                var item = sender as Picker;
                VM.spnrGender = item.SelectedItem.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To Define the Relationship picker Selected index change event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

            VM.spnrRelationship = selectedRelationship;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define the Language Selected Index changed event...
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

            VM.spnrLanguage = selectedLanguage;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    /// <summary>
    /// TODO : To define the state picker selected index change event....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void spnrState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            VM.spnrState = string.Empty;
            if (VM.patientProfile != null)
            {
                var item = sender as Picker;
                VM.spnrState = item.SelectedItem.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion 
}