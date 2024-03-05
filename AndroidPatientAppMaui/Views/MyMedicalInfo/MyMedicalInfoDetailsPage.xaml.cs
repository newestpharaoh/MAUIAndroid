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
            VM.medicalInfo = medicalInfo;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await VM.LoadMedicalIssues();
        }
        catch (Exception ex)
        {
             
        }
    }
    #endregion

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        try
        {

        }catch(Exception ex)
        {

        }
    }
}