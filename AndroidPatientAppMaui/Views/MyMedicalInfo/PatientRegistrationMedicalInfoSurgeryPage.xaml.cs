using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoSurgeryPage : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoSurgeryPage(Surgery surgery, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;

        VM.surgery = surgery;
    }
    #endregion

    #region Event Handler

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VM.DisplaySurguryDetails();
    }
    #endregion
}