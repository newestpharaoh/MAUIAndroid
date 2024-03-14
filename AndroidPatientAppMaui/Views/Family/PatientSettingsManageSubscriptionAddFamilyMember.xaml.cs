using AndroidPatientAppMaui.ViewModels.Family;

namespace AndroidPatientAppMaui.Views.Family;

public partial class PatientSettingsManageSubscriptionAddFamilyMember : ContentPage
{  //To define the class lavel variable.
    PatientSettingsManageSubscriptionAddFamilyMemberViewModel VM;
    #region Constructor
    public PatientSettingsManageSubscriptionAddFamilyMember()
	{
		InitializeComponent();
        this.BindingContext = VM = new PatientSettingsManageSubscriptionAddFamilyMemberViewModel(this.Navigation);
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
         //   await VM.GetUpdateFamilyMember();

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
                VM.patientProfile.Title = item.SelectedItem.ToString();
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
                VM.patientProfile.Gender = item.SelectedItem.ToString();
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

            VM.patientProfile.Relationship = selectedRelationship;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    } 
    #endregion 
}