using AndroidPatientAppMaui.ViewModels.Family;

namespace AndroidPatientAppMaui.Views.Family;

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