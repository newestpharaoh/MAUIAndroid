using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPharmacy : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoPharmacy(Pharmacy pharmacy, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;

        VM.pharmacy = pharmacy;
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.DisplayPharmacyDetails();
    }
    private void StatePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            VM.StateLbl = string.Empty;

            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //  VM.pharmacy.State = (string)picker.SelectedItem;
                VM.StateLbl = (string)picker.SelectedItem; 
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion
}