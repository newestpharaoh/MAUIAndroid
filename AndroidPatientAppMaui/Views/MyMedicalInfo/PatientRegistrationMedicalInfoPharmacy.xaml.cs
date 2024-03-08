using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPharmacy : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoPharmacy(IPatientRegistrationMedicalInfoItem item, int code, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;
    }
    #endregion

    #region Event Handler
    private void StatePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion
}