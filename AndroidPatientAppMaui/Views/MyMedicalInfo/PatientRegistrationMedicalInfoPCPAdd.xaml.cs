using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPAdd : ContentPage
{
	PatientRegistrationMedicalInfoPCPAddViewModel VM;
    public PatientRegistrationMedicalInfoPCPAdd(IPatientRegistrationMedicalInfoItem item, int code)
	{
		InitializeComponent();
        this.BindingContext = VM = new PatientRegistrationMedicalInfoPCPAddViewModel(this.Navigation);
    }
}