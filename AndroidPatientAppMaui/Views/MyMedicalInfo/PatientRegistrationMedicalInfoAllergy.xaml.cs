using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoAllergy : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
	public PatientRegistrationMedicalInfoAllergy(IPatientRegistrationMedicalInfoListItem item, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
		InitializeComponent();
        this.BindingContext = VM = pageViewModel;

    }
}