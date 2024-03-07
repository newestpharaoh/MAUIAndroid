using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoMedication : ContentPage
{
	PatientRegistrationMedicalInfoMedicationViewModel VM;
    public PatientRegistrationMedicalInfoMedication(IPatientRegistrationMedicalInfoListItem item, int code)
    {
		InitializeComponent();
        this.BindingContext = VM = new PatientRegistrationMedicalInfoMedicationViewModel(this.Navigation);
    }
}