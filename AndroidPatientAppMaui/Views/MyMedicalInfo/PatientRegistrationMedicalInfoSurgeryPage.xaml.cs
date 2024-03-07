using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoSurgeryPage : ContentPage
{
	PatientRegistrationMedicalInfoSurgeryViewModel VM;
    public PatientRegistrationMedicalInfoSurgeryPage(IPatientRegistrationMedicalInfoListItem item, int code)
    {
		InitializeComponent();
        this.BindingContext = VM = new PatientRegistrationMedicalInfoSurgeryViewModel(this.Navigation);
    }
}