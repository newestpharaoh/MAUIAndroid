using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoMedication : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoMedication(Medication medication, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;
        VM.medication = medication;
    }
    #endregion

    #region Event Handler

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.DisplayMedicalInfoDeails();
    }
    #endregion
}