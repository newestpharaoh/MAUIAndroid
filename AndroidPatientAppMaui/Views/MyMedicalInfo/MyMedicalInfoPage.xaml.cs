using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class MyMedicalInfoPage : ContentPage
{
    MyMedicalInfoPageViewModel VM;
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


    #region Event Handler
    #endregion
}