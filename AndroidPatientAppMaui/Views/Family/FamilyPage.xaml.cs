using AndroidPatientAppMaui.ViewModels.Family;

namespace AndroidPatientAppMaui.Views.Family;

public partial class FamilyPage : ContentPage
{

    FamilyPageViewModel VM;
    public  FamilyPage()
	{
		try
		{
			InitializeComponent();
			this.BindingContext = VM = new FamilyPageViewModel(this.Navigation);
		}
		catch (Exception)
		{
		}
    }

    #region Event Handler
    #endregion
}