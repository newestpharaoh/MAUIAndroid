using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class MyMedicalInfoDetailsPage : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public MyMedicalInfoDetailsPage(string title, MedicalInfo medicalInfo, AdditionalFamilyMember additionalFamilyMember)
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new MyMedicalInfoDetailsPageViewModel(this.Navigation);
            VM.medicalInfo = medicalInfo;
            VM.additionalFamilyMember = additionalFamilyMember;
            VM.UpdateList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    /// <summary>
    /// TODO : To define OnAppaering......
    /// </summary>
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.LoadMedicalIssues();
            await VM.DisplayMedicalInfoDeails();
        }
        catch (Exception ex)
        {

        }
    }
    /// <summary>
    /// TODO : To define Edit Pharmacy Click event.....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void imgEditPharmacy_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Pharmacy;
            if (VM.medicalInfo.Pharmacy.IsCurative || VM.patientIsCurative && !VM.medicalInfo.Pharmacy.IsCurative && VM.medicalInfo.Pharmacy.PharmacyString == "")
            {
                VM.ProcessCurative();
            }
            else if ((selecteditem).IsCapsule)
            {
                VM.ProcessCapsule();
            } 
            if (selecteditem != null)
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoPharmacy(selecteditem, VM.PHARMACY_REQUEST_CODE, VM), false);
            }
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// TODO : To define Edit Allergy Click event.....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void imgEditAllergy_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Allergy;
            if (selecteditem != null)
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoAllergy(selecteditem, VM.ALLERGY_REQUEST_CODE, VM), false);
            }
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// TODO : To define delete Allergy click event....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void imgDeleteAllergy_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Allergy;
            if (selecteditem != null)
            {
                //VM.DeleteMedicalListItem(selecteditem);
                foreach (var item in VM.AllergiesList)
                {
                    if (item.Name == selecteditem.Name && item.Description == selecteditem.Description)
                    {
                        string description = VM.GetDescription(item.GetType()) ?? string.Empty;
                        string title = VM.GetDialogTitle(item.GetType()) ?? string.Empty;
                        string confirmationMessage = $"Delete this {description.ToLower()}?";
                        bool answer = await Application.Current.MainPage.DisplayAlert(title, confirmationMessage, "Yes", "No");

                        if (answer)
                        {
                            VM.AllergiesList.Remove(item);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// TODO : To define Edit Medication Click event.....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void imgEditMedication_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Medication;
            if (selecteditem != null)
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoMedication(selecteditem, VM.MEDICATION_REQUEST_CODE, VM), false);
            }
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// TODO : To define Delete Medication click event......
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void imgDeleteMedication_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Medication;
            if (selecteditem != null)
            {
                foreach (var item in VM.MedicationsList)
                {
                    if (item.Name == selecteditem.Name && item.Description == selecteditem.Description)
                    {
                        string description = VM.GetDescription(item.GetType()) ?? string.Empty;
                        string title = VM.GetDialogTitle(item.GetType()) ?? string.Empty;
                        string confirmationMessage = $"Delete this {description.ToLower()}?";
                        bool answer = await Application.Current.MainPage.DisplayAlert(title, confirmationMessage, "Yes", "No");

                        if (answer)
                        {
                            VM.MedicationsList.Remove(item);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// TODO : To define Edit surgury click event.....
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void imgEditSurgery_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Surgery;
            if (selecteditem != null)
            {
                await Navigation.PushModalAsync(new Views.MyMedicalInfo.PatientRegistrationMedicalInfoSurgeryPage(selecteditem, VM.SURGERY_REQUEST_CODE, VM), false);
            }
        }
        catch (Exception ex)
        {
        }
    }
    /// <summary>
    /// TODO : To define delete surgury click event......
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void imgDeleteSurgery_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Surgery;
            if (selecteditem != null)
            {
                foreach (var item in VM.SurgeryList)
                {
                    if (item.Name == selecteditem.Name && item.Description == selecteditem.Description)
                    {
                        string description = VM.GetDescription(item.GetType()) ?? string.Empty;
                        string title = VM.GetDialogTitle(item.GetType()) ?? string.Empty;
                        string confirmationMessage = $"Delete this {description.ToLower()}?";
                        bool answer = await Application.Current.MainPage.DisplayAlert(title, confirmationMessage, "Yes", "No");

                        if (answer)
                        {
                            VM.SurgeryList.Remove(item);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    #endregion


}