using AndroidPatientAppMaui.ViewModels.MyMedicalInfo;

namespace AndroidPatientAppMaui.Views.MyMedicalInfo;

public partial class ProviderSelectionPage : ContentPage
{
	ProviderSelectionPageViewModel VM;
    public ProviderSelectionPage(string PatientName)
	{
		InitializeComponent();
        this.BindingContext = VM = new ProviderSelectionPageViewModel(this.Navigation);
    }
}