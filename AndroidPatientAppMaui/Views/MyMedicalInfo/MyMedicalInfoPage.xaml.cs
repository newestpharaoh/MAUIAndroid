using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class MyMedicalInfoPage : ContentPage
{
	//To define the class lavel variable.
	MyMedicalInfoPageViewModel VM;
	int patientId;

    #region Constructor
    public MyMedicalInfoPage()
	{
		try
		{

			InitializeComponent();
			this.BindingContext = VM = new MyMedicalInfoPageViewModel(this.Navigation);
		}
		catch (Exception)
		{
		}
	}
    #endregion

    #region Event Handler
	 
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await VM.DisplayMedicalInfo(patientId);
    }
    #endregion
}