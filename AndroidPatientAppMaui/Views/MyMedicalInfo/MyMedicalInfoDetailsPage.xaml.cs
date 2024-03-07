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
            await VM.DisplayAllergyDeails();
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

    private void imgEditAllergy_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Allergy;
            if (selecteditem != null)
            {
                VM.EditMedicalListItem(selecteditem);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void imgDeleteAllergy_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Allergy;
            if (selecteditem != null)
            {
                VM.DeleteMedicalListItem(selecteditem);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void imgEditMedication_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Medication;
            if (selecteditem != null)
            {
                VM.EditMedicalListItem(selecteditem);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void imgDeleteMedication_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Medication;
            if (selecteditem != null)
            {
                VM.DeleteMedicalListItem(selecteditem);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void imgEditSurgery_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Medication;
            if (selecteditem != null)
            {
                VM.EditMedicalListItem(selecteditem);
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void imgDeleteSurgery_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var selecteditem = (sender as Grid).BindingContext as Surgery;
            if (selecteditem != null)
            {
                VM.DeleteMedicalListItem(selecteditem);
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion


}