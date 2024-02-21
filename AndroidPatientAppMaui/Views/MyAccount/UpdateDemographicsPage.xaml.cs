using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateDemographicsPage : ContentPage
{
    //To define the class lavel variable.
    UpdateDemographicsPageViewModel VM;
    #region Constructor
    public UpdateDemographicsPage()
	{
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new UpdateDemographicsPageViewModel(this.Navigation);
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region Event Handler
    #endregion
}