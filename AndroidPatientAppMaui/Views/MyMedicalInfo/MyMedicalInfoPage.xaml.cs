using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class MyMedicalInfoPage : ContentPage
{
    //To define the class lavel variable.
    MyMedicalInfoPageViewModel VM;
   int PatientID = Helpers.AppGlobalConstants.userInfo.PatientID;
   
    #region Constructor
    public MyMedicalInfoPage(AccountMember am)
    {
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new MyMedicalInfoPageViewModel(this.Navigation);

            PatientID = am.PatientID;
            VM.am = am;
            //PatientID = Preferences.Get("PatientID", 0);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion

    #region Event Handler
    /// <summary>
    /// TODO : To define the on Appearing...
    /// </summary>
    protected async override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            VM.UserName = Helpers.AppGlobalConstants.userInfo.Name;
            await VM.DisplayMedicalInfo(PatientID, VM.am);
             VM.UpdateList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
    #endregion
}