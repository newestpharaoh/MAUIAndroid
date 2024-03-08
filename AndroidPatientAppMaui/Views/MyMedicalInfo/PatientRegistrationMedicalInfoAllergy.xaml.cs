using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoAllergy : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoAllergy(Allergy allergy, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;

        VM.allergy = allergy;
    }
    #endregion

    #region Event Handler

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.DisplayAllergyDeails();
    }
    #endregion
}