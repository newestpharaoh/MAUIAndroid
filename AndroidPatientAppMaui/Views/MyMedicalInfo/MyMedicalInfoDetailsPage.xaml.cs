using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class MyMedicalInfoDetailsPage : ContentPage
{
	MyMedicalInfoDetailsPageViewModel VM;
    public MyMedicalInfoDetailsPage(string title , MedicalInfo medicalInfo)
	{
		InitializeComponent();
        this.BindingContext = VM = new MyMedicalInfoDetailsPageViewModel(this.Navigation);
    }
}