using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPAdd : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoPCPAdd(IPatientRegistrationMedicalInfoItem item, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
	{
		InitializeComponent();
        this.BindingContext = VM = pageViewModel;
         
    }
    #endregion

    #region Event Handler

    protected override void OnAppearing()
    {
        base.OnAppearing(); 
    }
    #endregion

    private void StatePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}