using AndroidPatientAppMaui.ViewModels.Home;

namespace AndroidPatientAppMaui.Views.Home;

public partial class PatientPreVisitPatientSelectionStep1 : ContentPage
{
	PatientPreVisitPatientSelectionStep1ViewModel VM;
    #region Constructor
    public PatientPreVisitPatientSelectionStep1()
	{
		InitializeComponent(); 
        this.BindingContext = VM = new PatientPreVisitPatientSelectionStep1ViewModel(this.Navigation);
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.GetPaymentInformation();
    }
    #endregion
}