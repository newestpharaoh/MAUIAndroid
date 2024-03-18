using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPSelect : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoPCPSelect(string firstname, string lastname, string state, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;

        VM.pcp.FirstName = firstname;
        VM.pcp.LastName = lastname;
        VM.pcp.State = state;
    }
    #endregion
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.DisplayPCPSelectDetails();
    }
     
}