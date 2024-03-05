using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class MyMedicalInfoDetailsPage : ContentPage
{
	MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public MyMedicalInfoDetailsPage(string title , MedicalInfo medicalInfo)
	{
		try
		{
			InitializeComponent();
			this.BindingContext = VM = new MyMedicalInfoDetailsPageViewModel(this.Navigation);
		}
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    #endregion
}