using AndroidPatientApp.ViewModels.Family;

namespace AndroidPatientApp.Views.Family;

public partial class FamilyPage : ContentPage
{

    FamilyPageViewModel VM;
    public FamilyPage()
	{
		InitializeComponent();
        this.BindingContext = VM = new FamilyPageViewModel(this.Navigation);
    }

    #region Event Handler
    #endregion
}