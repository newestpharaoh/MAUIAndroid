using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPSearch : ContentPage
{
	PatientRegistrationMedicalInfoPCPSearchViewModel VM;
    public PatientRegistrationMedicalInfoPCPSearch(IPatientRegistrationMedicalInfoItem item, int code)
    {
		InitializeComponent();
        this.BindingContext = VM = new PatientRegistrationMedicalInfoPCPSearchViewModel(this.Navigation);
    }
}