using AndroidPatientAppMaui.ViewModels.MyAccount;


namespace AndroidPatientAppMaui.Views.MyAccount;

public partial class UpdateCardInformationPage : ContentPage
{
    //To define the class lavel variable.
    UpdateCardInformationPageViewModel VM;
    #region Constructor
    public UpdateCardInformationPage()
    {
        InitializeComponent();
        this.BindingContext = VM = new UpdateCardInformationPageViewModel(this.Navigation);
    }
    #endregion
    #region Event Handler

    #endregion
}