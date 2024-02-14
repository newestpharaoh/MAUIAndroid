using AndroidPatientApp.ViewModels.MyMedicalInfo;

namespace AndroidPatientApp.Views.MyMedicalInfo;

public partial class MyMedicalInfoPage : ContentPage
{
    MyMedicalInfoPageViewModel VM;
    public MyMedicalInfoPage()
	{
		InitializeComponent();
        this.BindingContext = VM = new MyMedicalInfoPageViewModel(this.Navigation);
    }


    #region Event Handler
    #endregion
}