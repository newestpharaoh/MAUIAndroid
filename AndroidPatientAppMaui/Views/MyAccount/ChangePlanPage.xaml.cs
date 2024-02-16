using AndroidPatientAppMaui.ViewModels.MyAccount;

namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class ChangePlanPage : ContentPage
{
    //To define the class lavel variable.
    ChangePlanPageViewModel VM;

    #region Constructor
    public ChangePlanPage()
    {
        InitializeComponent();
        this.BindingContext = VM = new ChangePlanPageViewModel(this.Navigation);
    }
    #endregion

    #region Event Handler

    #endregion
}