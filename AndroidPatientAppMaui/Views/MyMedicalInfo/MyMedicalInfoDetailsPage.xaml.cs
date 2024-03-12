using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class MyMedicalInfoDetailsPage : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public MyMedicalInfoDetailsPage(string title, MedicalInfo medicalInfo)
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new MyMedicalInfoDetailsPageViewModel(this.Navigation);
            VM.medicalInfo = medicalInfo;
            VM.UpdateList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
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

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {
            var chk = sender as CheckBox;
            //if (chk.Text.ToLower() == "none" && chk.Checked)
            //{
            //    medicalInfo.MedicalIssues.Clear();

            //    for (int i = 0; i < lytMedicalHistory.ChildCount; i++)
            //    {
            //        CheckBox checkbox = lytMedicalHistory.GetChildAt(i) as CheckBox;
            //        if (checkbox != null)
            //        {
            //            if (checkbox.Text.ToLower() != "none")
            //            {
            //                checkbox.Checked = false;
            //            }
            //        }
            //    }

            //    chkOtherMedicalIssue.Checked = false;
            //    txtOtherMedicalIssue.Enabled = false;

            //    txtOtherMedicalIssue.Text = string.Empty;

            //    VM.medicalInfo.MedicalIssues.Add((int)chk.Tag);
            //}
            //else
            //{
            //    if (chk.Checked)
            //    {
            //        medicalInfo.MedicalIssues.Add((int)chk.Tag);
            //        UncheckNoneCheckbox();
            //    }
            //    else
            //    {
            //        medicalInfo.MedicalIssues.Remove((int)chk.Tag);
            //    }
            //}
        }
        catch (Exception ex)
        {

        }
    }
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