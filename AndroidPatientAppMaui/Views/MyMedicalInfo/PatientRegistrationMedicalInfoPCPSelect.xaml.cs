using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class PatientRegistrationMedicalInfoPCPSelect : ContentPage
{
    MyMedicalInfoDetailsPageViewModel VM;
    #region Constructor
    public PatientRegistrationMedicalInfoPCPSelect(string firstname, string lastname, MyMedicalInfoDetailsPageViewModel pageViewModel)
    {
        InitializeComponent();
        this.BindingContext = VM = pageViewModel;

        VM.pcp.FirstName = firstname;
        VM.pcp.LastName = lastname; 
    }
    #endregion
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.DisplayPCPSelectDetails();
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}