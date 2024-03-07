using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPharmacy : ContentPage
{
	PatientRegistrationMedicalInfoPharmacyViewModel VM;
    public PatientRegistrationMedicalInfoPharmacy(IPatientRegistrationMedicalInfoItem item,int code)
	{
		InitializeComponent();
        this.BindingContext = VM = new PatientRegistrationMedicalInfoPharmacyViewModel(this.Navigation);
    }
}