using AndroidPatientAppMaui.ViewModels.MyAccount;
using CommonLibraryCoreMaui.Models;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateDemographicsPage : ContentPage
{
    //To define the class lavel variable.
    UpdateDemographicsPageViewModel VM;
    #region Constructor
    public UpdateDemographicsPage(BasicFamilyMemberInfo bfmi)
	{
        try
        {
            InitializeComponent();
            this.BindingContext = VM = new UpdateDemographicsPageViewModel(this.Navigation);
            VM.member = bfmi;
        }
        catch (Exception)
        {
        }
    }
    #endregion

    #region Event Handler
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        VM.UserName = Helpers.AppGlobalConstants.userInfo.Name;
        await VM.GetUpdateDemographics();
       
    }
    #endregion

    private void myDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {

    }
}