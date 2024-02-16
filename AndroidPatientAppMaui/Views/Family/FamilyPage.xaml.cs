using AndroidPatientAppMaui.ViewModels.Family;

namespace AndroidPatientAppMaui.Views.Family;

public partial class FamilyPage : ContentPage
{
    //To define the class lavel variable.
    FamilyPageViewModel VM;

    #region Constructor
    public FamilyPage()
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
    #endregion

    #region Event Handler
    #endregion
}